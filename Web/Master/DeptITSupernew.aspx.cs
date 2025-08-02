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
    public partial class DeptITSupernew : System.Web.UI.Page
    {
        #region Step1
        int count = 0;
        int take = 0;
        int Skip = 0;
        int TID = 0;
        public static int ChoiceID = 0;
        #endregion
        CallEntities DB = new CallEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            if (!IsPostBack)
            {
                Readonly();
                ManageLang();
                pnlSuccessMsg.Visible = false;
                FillContractorID();
                int CurrentID = 1;
                if (ViewState["Es"] != null)
                    CurrentID = Convert.ToInt32(ViewState["Es"]);
                BindData();
                btnAdd.ValidationGroup = "ss";
                //FirstData(); 

            }
        }
        #region Step2
        public void BindData()
        {
            List<Database.DeptITSuper> List = DB.DeptITSupers.Where(p=>p.TenentID == TID).OrderBy(m => m.DeptID).ToList();
            Listview1.DataSource = List;
            Listview1.DataBind();
            //int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
            //int Totalrec = List.Count();
            //((AcmMaster)Page.Master).Loadlist(Showdata, take, Skip, ChoiceID, lblShowinfEntry, btnPrevious1, btnNext1, Listview1, ListView2, Totalrec, List);
        }
        #endregion

        public void GetShow()
        {

            lblDeptName1s.Attributes["class"] = lblSuperVisorID1s.Attributes["class"] = "control-label col-md-4  getshow"; //lblSuperVisorName1s.Attributes["class"] = lblOperID1s.Attributes["class"] = lblOperName1s.Attributes["class"] = lblSpareUserID1s.Attributes["class"] = lblSpareUserName1s.Attributes["class"] = lblStatus1s.Attributes["class"] =
            lblDeptName2h.Attributes["class"] = lblSuperVisorID2h.Attributes["class"] = "control-label col-md-4  gethide"; //lblSuperVisorName2h.Attributes["class"] = lblOperID2h.Attributes["class"] = lblOperName2h.Attributes["class"] = lblSpareUserID2h.Attributes["class"] = lblSpareUserName2h.Attributes["class"] = lblStatus2h.Attributes["class"] =
            b.Attributes.Remove("dir");
            b.Attributes.Add("dir", "ltr");

        }

        public void GetHide()
        {
            lblDeptName1s.Attributes["class"] = lblSuperVisorID1s.Attributes["class"] = "control-label col-md-4  gethide"; //lblSuperVisorName1s.Attributes["class"] = lblOperID1s.Attributes["class"] = lblOperName1s.Attributes["class"] = lblSpareUserID1s.Attributes["class"] = lblSpareUserName1s.Attributes["class"] = lblStatus1s.Attributes["class"] = 
            lblDeptName2h.Attributes["class"] = lblSuperVisorID2h.Attributes["class"] = "control-label col-md-4  getshow"; //lblSuperVisorName2h.Attributes["class"] = lblOperID2h.Attributes["class"] = lblOperName2h.Attributes["class"] = lblSpareUserID2h.Attributes["class"] = lblSpareUserName2h.Attributes["class"] = lblStatus2h.Attributes["class"] =
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
            
            txtDeptName.Text = "";
            drpSuperVisorID.SelectedIndex = 0;
            //txtSuperVisorName.Text = "";
            txtmmail.Text = "";
            txtnmail.Text = "";
            txtcmail.Text = "";
            chkactive.Checked = false;
            txtsuper.Text = "";
        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    if (btnAdd.Text == "Add New")
                    {

                        Write();
                        Clear();
                        btnAdd.Text = "Save";
                        btnAdd.ValidationGroup = "submit";
                        pnlSuccessMsg.Visible = false;
                    }
                    else if (btnAdd.Text == "Save")
                    {
                        Database.DeptITSuper objDeptITSuper = new Database.DeptITSuper();
                        //Server Content Send data Yogesh
                        objDeptITSuper.TenentID = TID;
                        objDeptITSuper.DeptID = DB.DeptITSupers.Where(p => p.TenentID == TID).Count() > 0 ? Convert.ToInt32(DB.DeptITSupers.Where(p => p.TenentID == TID).Max(p => p.DeptID) + 1) : 1;
                        objDeptITSuper.DeptName = txtDeptName.Text;
                        objDeptITSuper.SuperVisorID = Convert.ToInt32(drpSuperVisorID.SelectedValue);
                        int eid = Convert.ToInt32(drpSuperVisorID.SelectedValue);
                        objDeptITSuper.SuperVisorName = txtsuper.Text;
                        objDeptITSuper.ManagerEmail = txtmmail.Text;
                        objDeptITSuper.NoticeEmail = txtnmail.Text;
                        objDeptITSuper.CoOrdintorEmail = txtcmail.Text;
                        if (chkactive.Checked == true)
                        {
                            objDeptITSuper.Status = "Start";
                        }
                        else
                        {
                            objDeptITSuper.Status = "Open";
                        }
                       
                        objDeptITSuper.ActivedBy = true;
                        objDeptITSuper.DeletedBy = true;
                        objDeptITSuper.DateTimee = DateTime.Now;
                        objDeptITSuper.Time = DateTime.Now;


                        DB.DeptITSupers.AddObject(objDeptITSuper);
                        DB.SaveChanges();

                        String url = "insert new record in DeptITSuper with " + "TenentID = " + TID + "DeptID = " + objDeptITSuper.DeptID;
                        String evantname = "create";
                        String tablename = "DeptITSuper";
                        string loginUserId = (((USER_MST)HttpContext.Current.Session["USER"]).USER_ID).ToString();

                        Classes.GlobleClass.EncryptionHelpers.WriteLog(url, evantname, tablename, loginUserId.ToString(), 0,0,1);

                        Clear();
                        lblMsg.Text = "  Data Save Successfully";
                        btnAdd.Text = "Add New";
                        btnAdd.ValidationGroup = "ss";
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
                            Database.DeptITSuper objDeptITSuper = DB.DeptITSupers.Single(p => p.DeptID == ID);
                            
                            objDeptITSuper.DeptName = txtDeptName.Text;
                            objDeptITSuper.SuperVisorID = Convert.ToInt32(drpSuperVisorID.SelectedValue);
                            int eid = Convert.ToInt32(drpSuperVisorID.SelectedValue);
                            objDeptITSuper.SuperVisorName = txtsuper.Text;
                            objDeptITSuper.NoticeEmail = txtnmail.Text;
                            objDeptITSuper.ManagerEmail = txtmmail.Text;
                            objDeptITSuper.CoOrdintorEmail = txtcmail.Text;
                            if (chkactive.Checked == true)
                            {
                                objDeptITSuper.Status = "Start";
                            }
                            else
                            {
                                objDeptITSuper.Status = "Open";
                            }
                            ViewState["Edit"] = null;
                            btnAdd.Text = "Add New";
                            DB.SaveChanges();

                            String url = "update DeptITSuper with " + "TenentID = " + TID + "DeptID = " + ID;
                            String evantname = "update";
                            String tablename = "DeptITSuper";
                            string loginUserId = (((USER_MST)HttpContext.Current.Session["USER"]).USER_ID).ToString();

                            Classes.GlobleClass.EncryptionHelpers.WriteLog(url, evantname, tablename, loginUserId.ToString(), 0,0,1);
                            Clear();
                            lblMsg.Text = "  Data Edit Successfully";
                            btnAdd.ValidationGroup = "ss";
                            pnlSuccessMsg.Visible = true;
                            BindData();
                            //navigation.Visible = true;
                            Readonly();
                            //FirstData();
                        }
                    }
                    scope.Complete(); //  To commit.

                }
                catch
                {
                   
                }
            }
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(Session["Previous"].ToString());
        }
        public void FillContractorID()
        {
            //drpTime.Items.Insert(0, new ListItem("-- Select --", "0"));drpTime.DataSource = DB.0;drpTime.DataTextField = "0";drpTime.DataValueField = "0";drpTime.DataBind();
            drpSuperVisorID.DataSource = DB.tbl_Employee.Where(p => p.TenentID == TID);
            drpSuperVisorID.DataTextField = "firstname";
            drpSuperVisorID.DataValueField = "employeeID";
            drpSuperVisorID.DataBind();
            drpSuperVisorID.Items.Insert(0, new ListItem("-- Select --", "0"));
        }
        protected void btnFirst_Click(object sender, EventArgs e)
        {
            //FirstData();
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
            
            txtDeptName.Text = Listview1.SelectedDataKey[0].ToString();
            drpSuperVisorID.SelectedValue = Listview1.SelectedDataKey[0].ToString();
            


        }
        public void NextData()
        {

            if (Listview1.SelectedIndex != Listview1.Items.Count - 1)
            {
                Listview1.SelectedIndex = Listview1.SelectedIndex + 1;
               
                txtDeptName.Text = Listview1.SelectedDataKey[0].ToString();
                drpSuperVisorID.SelectedValue = Listview1.SelectedDataKey[0].ToString();
               


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
               
                txtDeptName.Text = Listview1.SelectedDataKey[0].ToString();
                drpSuperVisorID.SelectedValue = Listview1.SelectedDataKey[0].ToString();
              
            }
        }
        public void LastData()
        {
            Listview1.SelectedIndex = Listview1.Items.Count - 1;
           
            txtDeptName.Text = Listview1.SelectedDataKey[0].ToString();
            drpSuperVisorID.SelectedValue = Listview1.SelectedDataKey[0].ToString();

        }


        protected void btnEditLable_Click(object sender, EventArgs e)
        {
            if (Session["LANGUAGE"].ToString() == "ar-KW")
            {
                if (btnEditLable.Text == "Update Label")
                {

                    //2false
                    lblDeptName2h.Visible = lblSuperVisorID2h.Visible = false;//lblSuperVisorName2h.Visible = lblOperID2h.Visible = lblOperName2h.Visible = lblSpareUserID2h.Visible = lblSpareUserName2h.Visible = lblStatus2h.Visible =
                    //2true
                    txtDeptName2h.Visible = txtSuperVisorID2h.Visible = true;//txtSuperVisorName2h.Visible = txtOperID2h.Visible = txtOperName2h.Visible = txtSpareUserID2h.Visible = txtSpareUserName2h.Visible = txtStatus2h.Visible = 

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
                    lblDeptName2h.Visible = lblSuperVisorID2h.Visible = true;//lblSuperVisorName2h.Visible = lblOperID2h.Visible = lblOperName2h.Visible = lblSpareUserID2h.Visible = lblSpareUserName2h.Visible = lblStatus2h.Visible = 
                    //2false
                    txtDeptName2h.Visible = txtSuperVisorID2h.Visible = false;//txtSuperVisorName2h.Visible = txtOperID2h.Visible = txtOperName2h.Visible = txtSpareUserID2h.Visible = txtSpareUserName2h.Visible = txtStatus2h.Visible = 

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
                    lblDeptName1s.Visible = lblSuperVisorID1s.Visible = false;//lblSuperVisorName1s.Visible = lblOperID1s.Visible = lblOperName1s.Visible = lblSpareUserID1s.Visible = lblSpareUserName1s.Visible = lblStatus1s.Visible = 
                    //1true
                    txtDeptName1s.Visible = txtSuperVisorID1s.Visible = true;//txtSuperVisorName1s.Visible = txtOperID1s.Visible = txtOperName1s.Visible = txtSpareUserID1s.Visible = txtSpareUserName1s.Visible = txtStatus1s.Visible = 
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
                    lblDeptName1s.Visible = lblSuperVisorID1s.Visible = true;//lblSuperVisorName1s.Visible = lblOperID1s.Visible = lblOperName1s.Visible = lblSpareUserID1s.Visible = lblSpareUserName1s.Visible = lblStatus1s.Visible = 
                    //1false
                    txtDeptName1s.Visible = txtSuperVisorID1s.Visible = false;//txtSuperVisorName1s.Visible = txtOperID1s.Visible = txtOperName1s.Visible = txtSpareUserID1s.Visible = txtSpareUserName1s.Visible = txtStatus1s.Visible = 
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

            List<Database.TBLLabelDTL> List = ((AcmMaster)this.Master).Bindxml("DeptITSuper").Where(p => p.LabelMstID == PID && p.LANGDISP == lang).ToList();
            foreach (Database.TBLLabelDTL item in List)
            {
                if (lblDeptName1s.ID == item.LabelID)
                    txtDeptName1s.Text = lblDeptName1s.Text = lblhDeptName.Text = item.LabelName;
                else if (lblSuperVisorID1s.ID == item.LabelID)
                    txtSuperVisorID1s.Text = lblSuperVisorID1s.Text = lblhSuperVisorID.Text = item.LabelName;
                //else if (lblSuperVisorName1s.ID == item.LabelID)
                //    txtSuperVisorName1s.Text = lblSuperVisorName1s.Text = item.LabelName;
                //else if (lblOperID1s.ID == item.LabelID)
                //    txtOperID1s.Text = lblOperID1s.Text = item.LabelName;
                //else if (lblOperName1s.ID == item.LabelID)
                //    txtOperName1s.Text = lblOperName1s.Text = item.LabelName;
                //else if (lblSpareUserID1s.ID == item.LabelID)
                //    txtSpareUserID1s.Text = lblSpareUserID1s.Text = item.LabelName;
                //else if (lblSpareUserName1s.ID == item.LabelID)
                //    txtSpareUserName1s.Text = lblSpareUserName1s.Text = item.LabelName;
                //else if (lblStatus1s.ID == item.LabelID)
                //    txtStatus1s.Text = lblStatus1s.Text = item.LabelName;

                else if (lblDeptName2h.ID == item.LabelID)
                    txtDeptName2h.Text = lblDeptName2h.Text = lblhDeptName.Text = item.LabelName;
                else if (lblSuperVisorID2h.ID == item.LabelID)
                    txtSuperVisorID2h.Text = lblSuperVisorID2h.Text = lblhSuperVisorID.Text = item.LabelName;
                //else if (lblSuperVisorName2h.ID == item.LabelID)
                //    txtSuperVisorName2h.Text = lblSuperVisorName2h.Text = item.LabelName;
                //else if (lblOperID2h.ID == item.LabelID)
                //    txtOperID2h.Text = lblOperID2h.Text = item.LabelName;
                //else if (lblOperName2h.ID == item.LabelID)
                //    txtOperName2h.Text = lblOperName2h.Text = item.LabelName;
                //else if (lblSpareUserID2h.ID == item.LabelID)
                //    txtSpareUserID2h.Text = lblSpareUserID2h.Text = item.LabelName;
                //else if (lblSpareUserName2h.ID == item.LabelID)
                //    txtSpareUserName2h.Text = lblSpareUserName2h.Text = item.LabelName;
                //else if (lblStatus2h.ID == item.LabelID)
                //    txtStatus2h.Text = lblStatus2h.Text = item.LabelName;

                else
                    txtHeader.Text = lblHeader.Text = Label5.Text = item.LabelName;
            }

        }
        public void SaveLabel(string lang)
        {
            string PID = ((AcmMaster)this.Master).getOwnPage();
            //List<Database.TBLLabelDTL> List = DB.TBLLabelDTLs.Where(p => p.LabelMstID == PID  && p.LANGDISP == lang).ToList();
            List<Database.TBLLabelDTL> List = ((AcmMaster)this.Master).Bindxml("DeptITSuper").Where(p => p.LabelMstID == PID && p.LANGDISP == lang).ToList();
            DataSet ds = new DataSet();
            ds.ReadXml(Server.MapPath("\\Master\\xml\\DeptITSuper.xml"));
            foreach (Database.TBLLabelDTL item in List)
            {

                var obj = ((AcmMaster)this.Master).Bindxml("DeptITSuper").Single(p => p.LabelID == item.LabelID && p.LabelMstID == PID && p.LANGDISP == lang);
                int i = obj.ID - 1;

                if (lblDeptName1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtDeptName1s.Text;
                else if (lblSuperVisorID1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtSuperVisorID1s.Text;
                //else if (lblSuperVisorName1s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtSuperVisorName1s.Text;
                //else if (lblOperID1s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtOperID1s.Text;
                //else if (lblOperName1s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtOperName1s.Text;
                //else if (lblSpareUserID1s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtSpareUserID1s.Text;
                //else if (lblSpareUserName1s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtSpareUserName1s.Text;
                //else if (lblStatus1s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtStatus1s.Text;

                else if (lblDeptName2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtDeptName2h.Text;
                else if (lblSuperVisorID2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtSuperVisorID2h.Text;
                //else if (lblSuperVisorName2h.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtSuperVisorName2h.Text;
                //else if (lblOperID2h.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtOperID2h.Text;
                //else if (lblOperName2h.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtOperName2h.Text;
                //else if (lblSpareUserID2h.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtSpareUserID2h.Text;
                //else if (lblSpareUserName2h.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtSpareUserName2h.Text;
                //else if (lblStatus2h.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtStatus2h.Text;

                else
                    ds.Tables[0].Rows[i]["LabelName"] = txtHeader.Text;
            }
            ds.WriteXml(Server.MapPath("\\Master\\xml\\DeptITSuper.xml"));

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
            txtDeptName.Enabled = true;
            drpSuperVisorID.Enabled = true;
            txtcmail.Enabled = true;
            txtmmail.Enabled = true;
            txtnmail.Enabled = true;
            txtsuper.Enabled = true;
        }
        public void Readonly()
        {
            txtDeptName.Enabled = false;
            drpSuperVisorID.Enabled = false;
            txtcmail.Enabled = false;
            txtmmail.Enabled = false;
            txtnmail.Enabled = false;
            txtsuper.Enabled = false;
        }

        #region Listview
        //protected void btnNext1_Click(object sender, EventArgs e)
        //{

        //    int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
        //    int Totalrec = DB.DeptITSupers.Count();
        //    if (ViewState["Take"] == null && ViewState["Skip"] == null)
        //    {
        //        ViewState["Take"] = Showdata;
        //        ViewState["Skip"] = 0;
        //    }
        //    take = Convert.ToInt32(ViewState["Take"]);
        //    take = take + Showdata;
        //    Skip = take - Showdata;

        //    ((AcmMaster)Page.Master).BindList(Listview1, (DB.DeptITSuper.OrderBy(m => m.DeptID).Take(take).Skip(Skip)).ToList());
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

        //    ((AcmMaster)Page.Master).GetCurrentNavigation(ChoiceID, Showdata, ListView2);
        //    lblShowinfEntry.Text = "Showing " + Skip + " to " + take + " of " + Totalrec + " entries";

        //}
        //protected void btnPrevious1_Click(object sender, EventArgs e)
        //{

        //    int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
        //    if (ViewState["Take"] != null && ViewState["Skip"] != null)
        //    {
        //        int Totalrec = DB.DeptITSuper.Count();
        //        Skip = Convert.ToInt32(ViewState["Skip"]);
        //        take = Skip;
        //        Skip = take - Showdata;
        //        ((AcmMaster)Page.Master).BindList(Listview1, (DB.DeptITSuper.OrderBy(m => m.DeptID).Take(take).Skip(Skip)).ToList());
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
        //        ((AcmMaster)Page.Master).GetCurrentNavigation(ChoiceID, Showdata, ListView2);
        //        lblShowinfEntry.Text = "Showing " + Skip + " to " + take + " of " + Totalrec + " entries";
        //    }
        //}
        //protected void btnfirst_Click(object sender, EventArgs e)
        //{

        //    int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
        //    if (ViewState["Take"] != null && ViewState["Skip"] != null)
        //    {
        //        int Totalrec = DB.DeptITSuper.Count();
        //        take = Showdata;
        //        Skip = 0;
        //        ((AcmMaster)Page.Master).BindList(Listview1, (DB.DeptITSuper.OrderBy(m => m.DeptID).Take(take).Skip(Skip)).ToList());
        //        ViewState["Take"] = take;
        //        ViewState["Skip"] = Skip;
        //        btnPrevious1.Enabled = false;
        //        ChoiceID = 0;
        //        ((AcmMaster)Page.Master).GetCurrentNavigation(ChoiceID, Showdata, ListView2);
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
        //    int Totalrec = DB.DeptITSuper.Count();
        //    take = Totalrec;
        //    Skip = Totalrec - Showdata;
        //    ((AcmMaster)Page.Master).BindList(Listview1, (DB.DeptITSuper.OrderBy(m => m.DeptID).Take(take).Skip(Skip)).ToList());
        //    ViewState["Take"] = take;
        //    ViewState["Skip"] = Skip;
        //    btnNext1.Enabled = false;
        //    btnPrevious1.Enabled = true;
        //    ChoiceID = take / Showdata;
        //    ((AcmMaster)Page.Master).GetCurrentNavigationLast(ChoiceID, Showdata, ListView2);
        //    lblShowinfEntry.Text = "Showing " + ViewState["Skip"].ToString() + " to " + ViewState["Take"].ToString() + " of " + Totalrec + " entries";
        //}
        //protected void btnlistreload_Click(object sender, EventArgs e)
        //{
        //    BindData();
        //}
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
            //FirstData();
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
                        Database.DeptITSuper objSOJobDesc = DB.DeptITSupers.Single(p => p.TenentID == TID && p.DeptID == ID);
                        objSOJobDesc.DeletedBy = false;
                        DB.SaveChanges();

                        String url = "delete DeptITSuper with " + "TenentID = " + TID + "DeptID = " + ID;
                        String evantname = "delete";
                        String tablename = "DeptITSuper";
                        string loginUserId = (((USER_MST)HttpContext.Current.Session["USER"]).USER_ID).ToString();

                        Classes.GlobleClass.EncryptionHelpers.WriteLog(url, evantname, tablename, loginUserId.ToString(), 0,0,1);

                        BindData();
                    }
                    if (e.CommandName == "btnEdit")
                    {
                        int ID = Convert.ToInt32(e.CommandArgument);
                        Database.DeptITSuper objDeptITSuper = DB.DeptITSupers.Single(p => p.TenentID == TID && p.DeptID == ID);
                        txtDeptName.Text = objDeptITSuper.DeptName.ToString();
                        drpSuperVisorID.SelectedValue = objDeptITSuper.SuperVisorID.ToString();
                        txtsuper.Text = objDeptITSuper.SuperVisorName;
                        txtnmail.Text = objDeptITSuper.NoticeEmail;
                        txtmmail.Text = objDeptITSuper.ManagerEmail;
                        txtcmail.Text = objDeptITSuper.CoOrdintorEmail;
                        if (objDeptITSuper.Status == "Start")
                        {
                            chkactive.Checked = true;
                        }
                        else
                        {
                            chkactive.Checked = false;
                        }
                        btnAdd.Text = "Update";
                        ViewState["Edit"] = ID;
                        btnAdd.ValidationGroup = "submit";
                        Write();
                    }
                    scope.Complete(); //  To commit.
                }
                catch
                {
                   
                }
            }
        }

        //protected void ListView2_ItemCommand(object sender, ListViewCommandEventArgs e)
        //{

        //    int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
        //    int Totalrec = DB.DeptITSuper.Count();
        //    if (e.CommandName == "LinkPageavigation")
        //    {
        //        int ID = Convert.ToInt32(e.CommandArgument);
        //        ViewState["Take"] = ID * Showdata;
        //        ViewState["Skip"] = (ID * Showdata) - Showdata;
        //        int Tvalue = Convert.ToInt32(ViewState["Take"]);
        //        int Svalue = Convert.ToInt32(ViewState["Skip"]);
        //        ((AcmMaster)Page.Master).BindList(Listview1, (DB.DeptITSuper.OrderBy(m => m.DeptID).Take(Tvalue).Skip(Svalue)).ToList());
        //        ChoiceID = ID;
        //        ((AcmMaster)Page.Master).GetCurrentNavigation(ChoiceID, Showdata, ListView2);
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

    }
}