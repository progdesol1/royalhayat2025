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
    public partial class TBLMaintanance : System.Web.UI.Page
    {
        #region Step1
        int count = 0;
        int take = 0;
        int Skip = 0;
        public static int ChoiceID = 0;
        #endregion
        CallEntities DB = new CallEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
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
                getdata();
                FirstData();

            }
        }
        #region Step2
        public void BindData()
        {
            List<Database.TBLMaintanance> List = DB.TBLMaintanances.Where(p=>p.Deleted==true).ToList();
            int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
            int Totalrec = List.Count();
            ((AcmMaster)Page.Master).Loadlist(Showdata, take, Skip, ChoiceID, lblShowinfEntry, btnPrevious1, btnNext1, Listview1, ListView2, Totalrec, List);
        }
        #endregion

        public void GetShow()
        {

            lblID1s.Attributes["class"] = lblTitle1s.Attributes["class"] = lblQuery1s.Attributes["class"] = lblDescription1s.Attributes["class"] =  lblcbActive1s.Attributes["class"] =  lblMODULEID1s.Attributes["class"] = lblSwichType1s.Attributes["class"] = "control-label col-md-4  getshow";
            lblID2h.Attributes["class"] = lblTitle2h.Attributes["class"] = lblQuery2h.Attributes["class"] = lblDescription2h.Attributes["class"] = lblcbActive2h.Attributes["class"] = lblMODULEID2h.Attributes["class"] = lblSwichType2h.Attributes["class"] = "control-label col-md-4  gethide";
            b.Attributes.Remove("dir");
            b.Attributes.Add("dir", "ltr");

        }

        public void GetHide()
        {
            lblID1s.Attributes["class"] = lblTitle1s.Attributes["class"] = lblQuery1s.Attributes["class"] = lblDescription1s.Attributes["class"] =  lblcbActive1s.Attributes["class"] =  lblMODULEID1s.Attributes["class"] = lblSwichType1s.Attributes["class"] = "control-label col-md-4  gethide";
            lblID2h.Attributes["class"] = lblTitle2h.Attributes["class"] = lblQuery2h.Attributes["class"] = lblDescription2h.Attributes["class"] = lblcbActive2h.Attributes["class"] =  lblMODULEID2h.Attributes["class"] = lblSwichType2h.Attributes["class"] = "control-label col-md-4  getshow";
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
            txtTitle.Text = "";
            txtQuery.Text = "";
            txtDescription.Text = "";
            //txtDateTime.Text = "";
            cbActive.Text = "";
            //txtDeleted.Text = "";
            drpMODULEID.SelectedIndex = 0;
            drpSwichType.SelectedIndex = 0;

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
                        Clear();
                        btnAdd.Text = "Add";
                        btnAdd.ValidationGroup = "s";
                    }
                    else if (btnAdd.Text == "Add")
                    {
                        Database.TBLMaintanance objTBLMaintanance = new Database.TBLMaintanance();
                        //Server Content Send data Yogesh
                        objTBLMaintanance.Title = txtTitle.Text;
                        objTBLMaintanance.Query = txtQuery.Text;
                        objTBLMaintanance.Description = txtDescription.Text;
                        objTBLMaintanance.DateTime = DateTime.Now;
                        objTBLMaintanance.Active = cbActive.Checked;
                        objTBLMaintanance.Deleted = true;
                        objTBLMaintanance.MODULEID = Convert.ToInt32(drpMODULEID.SelectedValue);
                        objTBLMaintanance.SwichType = Convert.ToInt32(drpSwichType.SelectedValue);


                        DB.TBLMaintanances.AddObject(objTBLMaintanance);
                        DB.SaveChanges();
                        Clear();
                        btnAdd.Text = "Add New";
                        btnAdd.ValidationGroup = "submit";
                        //lblMsg.Text = "  Data Save Successfully";
                        //pnlSuccessMsg.Visible = true;
                        Classes.Toastr.ShowToast(Page, Classes.Toastr.ToastType.Success, "Data Save Successfully", "Success!", Classes.Toastr.ToastPosition.TopCenter);
                        BindData();
                        //navigation.Visible = true;
                        Readonly();
                        FirstData();
                    }
                    else if (btnAdd.Text == "Update")
                    {

                        if (ViewState["Edit"] != null)
                        {
                            int ID = Convert.ToInt32(ViewState["Edit"]);
                            Database.TBLMaintanance objTBLMaintanance = DB.TBLMaintanances.Single(p => p.ID == ID);
                            objTBLMaintanance.Title = txtTitle.Text;
                            objTBLMaintanance.Query = txtQuery.Text;
                            objTBLMaintanance.Description = txtDescription.Text;
                            objTBLMaintanance.DateTime = DateTime.Now;
                            objTBLMaintanance.Active = cbActive.Checked;
                            objTBLMaintanance.Deleted = true;
                            objTBLMaintanance.MODULEID = Convert.ToInt32(drpMODULEID.SelectedValue);
                            objTBLMaintanance.SwichType = Convert.ToInt32(drpSwichType.SelectedValue);
                            
                            ViewState["Edit"] = null;
                            DB.SaveChanges();
                            Clear();
                            btnAdd.Text = "Add New";
                            btnAdd.ValidationGroup = "submit";
                            //lblMsg.Text = "  Data Edit Successfully";
                            //pnlSuccessMsg.Visible = true;
                            Classes.Toastr.ShowToast(Page, Classes.Toastr.ToastType.Success, "Data Edit Successfully", "Success!", Classes.Toastr.ToastPosition.TopCenter);
                            BindData();
                            //navigation.Visible = true;
                            Readonly();
                            FirstData();
                        }
                    }
                    BindData();

                    scope.Complete(); //  To commit.

                }
            //    catch (Exception ex)
            //    {
            //        throw;
            //    }
            //}
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(Session["Previous"].ToString());
        }
        public void FillContractorID()
        {
            //drpSwichType.Items.Insert(0, new ListItem("-- Select --", "0"));drpSwichType.DataSource = DB.0;drpSwichType.DataTextField = "0";drpSwichType.DataValueField = "0";drpSwichType.DataBind();
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

        public void getdata()
        {
            drpMODULEID.DataSource = DB.MODULE_MST.Where(p => p.TenentID==1);
            drpMODULEID.DataTextField = "Module_Name";
            drpMODULEID.DataValueField = "Module_Id";
            drpMODULEID.DataBind();
            drpMODULEID.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select Size--", "0"));

            //drpSwichType.DataSource = DB.MODULE_MST.Where(p => p.TENANT_ID == 1);
            //drpSwichType.DataTextField = "Module_Name";
            //drpSwichType.DataValueField = "Module_Id";
            //drpSwichType.DataBind();
            //drpSwichType.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select Size--", "0"));


        }
        public string gatdataa(int MODULEID)
        {
            string modual=DB.MODULE_MST.Single(p => p.TenentID == 1 && p.Module_Id == MODULEID).Module_Name;
            return modual;
        }


        public void FirstData()
        {
            int index = Convert.ToInt32(ViewState["Index"]);
            Listview1.SelectedIndex = 0;
            txtTitle.Text = Listview1.SelectedDataKey[0].ToString();
            txtQuery.Text = Listview1.SelectedDataKey[0].ToString();
            txtDescription.Text = Listview1.SelectedDataKey[0].ToString();
            //txtDateTime.Text = Listview1.SelectedDataKey[0].ToString();
            cbActive.Checked =Convert.ToBoolean( Listview1.SelectedDataKey[0]);
            drpMODULEID.SelectedValue = Listview1.SelectedDataKey[0].ToString();
            drpSwichType.SelectedValue = Listview1.SelectedDataKey[0].ToString();

        }
        public void NextData()
        {

            if (Listview1.SelectedIndex != Listview1.Items.Count - 1)
            {
                Listview1.SelectedIndex = Listview1.SelectedIndex + 1;
                txtTitle.Text = Listview1.SelectedDataKey[0].ToString();
                txtQuery.Text = Listview1.SelectedDataKey[0].ToString();
                txtDescription.Text = Listview1.SelectedDataKey[0].ToString();
                //txtDateTime.Text = Listview1.SelectedDataKey[0].ToString();
                cbActive.Checked = Convert.ToBoolean(Listview1.SelectedDataKey[0]);
                drpMODULEID.SelectedValue = Listview1.SelectedDataKey[0].ToString();
                drpSwichType.SelectedValue = Listview1.SelectedDataKey[0].ToString();

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
                txtTitle.Text = Listview1.SelectedDataKey[0].ToString();
                txtQuery.Text = Listview1.SelectedDataKey[0].ToString();
                txtDescription.Text = Listview1.SelectedDataKey[0].ToString();
                //txtDateTime.Text = Listview1.SelectedDataKey[0].ToString();
                cbActive.Checked =Convert.ToBoolean(Listview1.SelectedDataKey[0]);
                drpMODULEID.SelectedValue = Listview1.SelectedDataKey[0].ToString();
                drpSwichType.SelectedValue = Listview1.SelectedDataKey[0].ToString();

            }
        }
        public void LastData()
        {
            Listview1.SelectedIndex = Listview1.Items.Count - 1;
            txtTitle.Text = Listview1.SelectedDataKey[0].ToString();
            txtQuery.Text = Listview1.SelectedDataKey[0].ToString();
            txtDescription.Text = Listview1.SelectedDataKey[0].ToString();
            //obj.DateTime = DateTime.Now;
            cbActive.Checked = Convert.ToBoolean(Listview1.SelectedDataKey[0]);
            drpMODULEID.SelectedValue = Listview1.SelectedDataKey[0].ToString();
            drpSwichType.SelectedValue = Listview1.SelectedDataKey[0].ToString();

        }


        protected void btnEditLable_Click(object sender, EventArgs e)
        {
            if (Session["LANGUAGE"].ToString() == "ar-KW")
            {
                if (btnEditLable.Text == "Update Label")
                {

                    //2false
                    lblID2h.Visible = lblTitle2h.Visible = lblQuery2h.Visible = lblDescription2h.Visible =  lblcbActive2h.Visible =  lblMODULEID2h.Visible = lblSwichType2h.Visible = false;
                    //2true
                    txtID2h.Visible = txtTitle2h.Visible = txtQuery2h.Visible = txtDescription2h.Visible =  cbActive2h.Visible = drpMODULEID2h.Visible = drpSwichType2h.Visible = true;

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
                    lblID2h.Visible = lblTitle2h.Visible = lblQuery2h.Visible = lblDescription2h.Visible =  lblcbActive2h.Visible =  lblMODULEID2h.Visible = lblSwichType2h.Visible = true;
                    //2false
                    txtID2h.Visible = txtTitle2h.Visible = txtQuery2h.Visible = txtDescription2h.Visible =  cbActive2h.Visible =  drpMODULEID2h.Visible = drpSwichType2h.Visible = false;

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
                    lblID1s.Visible = lblTitle1s.Visible = lblQuery1s.Visible = lblDescription1s.Visible = lblcbActive1s.Visible =  lblMODULEID1s.Visible = lblSwichType1s.Visible = false;
                    //1true
                    txtID1s.Visible = txtTitle1s.Visible = txtQuery1s.Visible = txtDescription1s.Visible = cbActive1s.Visible = drpMODULEID1s.Visible = drpSwichType1s.Visible = true;
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
                    lblID1s.Visible = lblTitle1s.Visible = lblQuery1s.Visible = lblDescription1s.Visible =  lblcbActive1s.Visible =  lblMODULEID1s.Visible = lblSwichType1s.Visible = true;
                    //1false
                    txtID1s.Visible = txtTitle1s.Visible = txtQuery1s.Visible = txtDescription1s.Visible =  cbActive1s.Visible =  drpMODULEID1s.Visible = drpSwichType1s.Visible = false;
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

            List<Database.TBLLabelDTL> List = ((AcmMaster)this.Master).Bindxml("TBLMaintanance").Where(p => p.LabelMstID == PID && p.LANGDISP == lang).ToList();
            foreach (Database.TBLLabelDTL item in List)
            {
                if (lblID1s.ID == item.LabelID)
                    txtID1s.Text = lblID1s.Text = lblhID.Text = item.LabelName;
                else if (lblTitle1s.ID == item.LabelID)
                    txtTitle1s.Text = lblTitle1s.Text = lblhTitle.Text = item.LabelName;
                else if (lblQuery1s.ID == item.LabelID)
                    txtQuery1s.Text = lblQuery1s.Text = lblhQuery.Text = item.LabelName;
                else if (lblDescription1s.ID == item.LabelID)
                    txtDescription1s.Text = lblDescription1s.Text = lblhDescription.Text = item.LabelName;
                //else if (lblDateTime1s.ID == item.LabelID)
                //    txtDateTime1s.Text = lblDateTime1s.Text = lblhDateTime.Text = item.LabelName;
                else if (lblcbActive1s.ID == item.LabelID)
                    cbActive1s.Text = lblcbActive1s.Text = lblhActive.Text = item.LabelName;
                //else if (lblDeleted1s.ID == item.LabelID)
                //    txtDeleted1s.Text = lblDeleted1s.Text = lblhDeleted.Text = item.LabelName;
                else if (lblMODULEID1s.ID == item.LabelID)
                    drpMODULEID1s.Text = lblMODULEID1s.Text = lblhMODULEID.Text = item.LabelName;
                else if (lblSwichType1s.ID == item.LabelID)
                    drpSwichType1s.Text = lblSwichType1s.Text = lblhSwichType.Text = item.LabelName;

                else if (lblID2h.ID == item.LabelID)
                    txtID2h.Text = lblID2h.Text = lblhID.Text = item.LabelName;
                else if (lblTitle2h.ID == item.LabelID)
                    txtTitle2h.Text = lblTitle2h.Text = lblhTitle.Text = item.LabelName;
                else if (lblQuery2h.ID == item.LabelID)
                    txtQuery2h.Text = lblQuery2h.Text = lblhQuery.Text = item.LabelName;
                else if (lblDescription2h.ID == item.LabelID)
                    txtDescription2h.Text = lblDescription2h.Text = lblhDescription.Text = item.LabelName;
                //else if (lblDateTime2h.ID == item.LabelID)
                //    txtDateTime2h.Text = lblDateTime2h.Text = lblhDateTime.Text = item.LabelName;
                else if (lblcbActive2h.ID == item.LabelID)
                    cbActive2h.Text = lblcbActive2h.Text = lblhActive.Text = item.LabelName;
                //else if (lblDeleted2h.ID == item.LabelID)
                //    txtDeleted2h.Text = lblDeleted2h.Text = lblhDeleted.Text = item.LabelName;
                else if (lblMODULEID2h.ID == item.LabelID)
                    drpMODULEID2h.Text = lblMODULEID2h.Text = lblhMODULEID.Text = item.LabelName;
                else if (lblSwichType2h.ID == item.LabelID)
                    drpSwichType2h.Text = lblSwichType2h.Text = lblhSwichType.Text = item.LabelName;

                else
                    txtHeader.Text = lblHeader.Text = Label5.Text = item.LabelName;
            }

        }
        public void SaveLabel(string lang)
        {
            string PID = ((AcmMaster)this.Master).getOwnPage();
            //List<Database.TBLLabelDTL> List = DB.TBLLabelDTLs.Where(p => p.LabelMstID == PID  && p.LANGDISP == lang).ToList();
            List<Database.TBLLabelDTL> List = ((AcmMaster)this.Master).Bindxml("TBLMaintanance").Where(p => p.LabelMstID == PID && p.LANGDISP == lang).ToList();
            DataSet ds = new DataSet();
            ds.ReadXml(Server.MapPath("\\Master\\xml\\TBLMaintanance.xml"));
            foreach (Database.TBLLabelDTL item in List)
            {
                var obj = ((AcmMaster)this.Master).Bindxml("TBLMaintanance").Single(p => p.LabelID == item.LabelID && p.LabelMstID == PID && p.LANGDISP == lang);
                int i = obj.ID - 1;

                if (lblID1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtID1s.Text;
                else if (lblTitle1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtTitle1s.Text;
                else if (lblQuery1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtQuery1s.Text;
                else if (lblDescription1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtDescription1s.Text;
                //else if (lblDateTime1s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtDateTime1s.Text;
                else if (lblcbActive1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = cbActive1s.Text;
                //else if (lblDeleted1s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtDeleted1s.Text;
                else if (lblMODULEID1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = drpMODULEID1s.Text;
                else if (lblSwichType1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = drpSwichType1s.Text;

                else if (lblID2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtID2h.Text;
                else if (lblTitle2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtTitle2h.Text;
                else if (lblQuery2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtQuery2h.Text;
                else if (lblDescription2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtDescription2h.Text;
                //else if (lblDateTime2h.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtDateTime2h.Text;
                else if (lblcbActive2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = cbActive2h.Text;
                //else if (lblDeleted2h.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtDeleted2h.Text;
                else if (lblMODULEID2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = drpMODULEID2h.Text;
                else if (lblSwichType2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = drpSwichType2h.Text;

                else
                    ds.Tables[0].Rows[i]["LabelName"] = txtHeader.Text;
            }
            ds.WriteXml(Server.MapPath("\\Master\\xml\\TBLMaintanance.xml"));

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
            txtTitle.Enabled = true;
            txtQuery.Enabled = true;
            txtDescription.Enabled = true;
            //txtDateTime.Enabled = true;
            cbActive.Enabled = true;
            drpMODULEID.Enabled = true;
            drpSwichType.Enabled = true;

        }
        public void Readonly()
        {
            //navigation.Visible = true;
            txtTitle.Enabled = false;
            txtQuery.Enabled = false;
            txtDescription.Enabled = false;
            //txtDateTime.Enabled = false;
            cbActive.Enabled = false;
            drpMODULEID.Enabled = false;
            drpSwichType.Enabled = false;


        }

        #region Listview
        protected void btnNext1_Click(object sender, EventArgs e)
        {

            int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
            int Totalrec = DB.TBLMaintanances.Count();
            if (ViewState["Take"] == null && ViewState["Skip"] == null)
            {
                ViewState["Take"] = Showdata;
                ViewState["Skip"] = 0;
            }
            take = Convert.ToInt32(ViewState["Take"]);
            take = take + Showdata;
            Skip = take - Showdata;

            ((AcmMaster)Page.Master).BindList(Listview1, (DB.TBLMaintanances.OrderBy(m => m.ID).Take(take).Skip(Skip)).ToList());
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

            //((AcmMaster)Page.Master).GetCurrentNavigation(ChoiceID, Showdata, ListView2);
            lblShowinfEntry.Text = "Showing " + Skip + " to " + take + " of " + Totalrec + " entries";

        }
        protected void btnPrevious1_Click(object sender, EventArgs e)
        {

            int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
            if (ViewState["Take"] != null && ViewState["Skip"] != null)
            {
                int Totalrec = DB.TBLMaintanances.Count();
                Skip = Convert.ToInt32(ViewState["Skip"]);
                take = Skip;
                Skip = take - Showdata;
                ((AcmMaster)Page.Master).BindList(Listview1, (DB.TBLMaintanances.OrderBy(m => m.ID).Take(take).Skip(Skip)).ToList());
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
                //((AcmMaster)Page.Master).GetCurrentNavigation(ChoiceID, Showdata, ListView2);
                lblShowinfEntry.Text = "Showing " + Skip + " to " + take + " of " + Totalrec + " entries";
            }
        }
        protected void btnfirst_Click(object sender, EventArgs e)
        {

            int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
            if (ViewState["Take"] != null && ViewState["Skip"] != null)
            {
                int Totalrec = DB.TBLMaintanances.Count();
                take = Showdata;
                Skip = 0;
                ((AcmMaster)Page.Master).BindList(Listview1, (DB.TBLMaintanances.OrderBy(m => m.ID).Take(take).Skip(Skip)).ToList());
                ViewState["Take"] = take;
                ViewState["Skip"] = Skip;
                btnPrevious1.Enabled = false;
                ChoiceID = 0;
                //((AcmMaster)Page.Master).GetCurrentNavigation(ChoiceID, Showdata, ListView2);
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
            int Totalrec = DB.TBLMaintanances.Count();
            take = Totalrec;
            Skip = Totalrec - Showdata;
            ((AcmMaster)Page.Master).BindList(Listview1, (DB.TBLMaintanances.OrderBy(m => m.ID).Take(take).Skip(Skip)).ToList());
            ViewState["Take"] = take;
            ViewState["Skip"] = Skip;
            btnNext1.Enabled = false;
            btnPrevious1.Enabled = true;
            ChoiceID = take / Showdata;
            //((AcmMaster)Page.Master).GetCurrentNavigationLast(ChoiceID, Showdata, ListView2);
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
                        int ID = Convert.ToInt32(e.CommandArgument);
                        Database.TBLMaintanance objSOJobDesc = DB.TBLMaintanances.Single(p => p.ID == ID && p.Deleted==true);
                        objSOJobDesc.Deleted = false;
                        DB.SaveChanges();
                        Classes.Toastr.ShowToast(Page, Classes.Toastr.ToastType.Success, "Data Deleted Successfully", "Deleted!", Classes.Toastr.ToastPosition.TopCenter);
                        BindData();
                        int Tvalue = Convert.ToInt32(ViewState["Take"]);
                        int Svalue = Convert.ToInt32(ViewState["Skip"]);
                        ((AcmMaster)Page.Master).BindList(Listview1, (DB.TBLMaintanances.OrderBy(m => m.ID).Take(Tvalue).Skip(Svalue)).ToList());

                    }

                    if (e.CommandName == "btnEdit")
                    {
                        //string[] ID = e.CommandArgument.ToString().Split(',');
                        //string str1 = ID[0].ToString();
                        //string str2 = ID[1].ToString();

                        int ID = Convert.ToInt32(e.CommandArgument);
                        Database.TBLMaintanance objTBLMaintanance = DB.TBLMaintanances.Single(p => p.ID == ID);
                        txtTitle.Text = objTBLMaintanance.Title.ToString();
                        txtQuery.Text = objTBLMaintanance.Query.ToString();
                        txtDescription.Text = objTBLMaintanance.Description.ToString();
                        //txtDateTime.Text = objTBLMaintanance.DateTime.ToString();
                        cbActive.Checked = (objTBLMaintanance.Active == true) ? true : false;
                        drpMODULEID.SelectedValue = objTBLMaintanance.MODULEID.ToString();
                        drpSwichType.SelectedValue = objTBLMaintanance.SwichType.ToString();

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
            int Totalrec = DB.TBLMaintanances.Count();
            if (e.CommandName == "LinkPageavigation")
            {
                int ID = Convert.ToInt32(e.CommandArgument);
                ViewState["Take"] = ID * Showdata;
                ViewState["Skip"] = (ID * Showdata) - Showdata;
                int Tvalue = Convert.ToInt32(ViewState["Take"]);
                int Svalue = Convert.ToInt32(ViewState["Skip"]);
                ((AcmMaster)Page.Master).BindList(Listview1, (DB.TBLMaintanances.OrderBy(m => m.ID).Take(Tvalue).Skip(Svalue)).ToList());
                ChoiceID = ID;
                //((AcmMaster)Page.Master).GetCurrentNavigation(ChoiceID, Showdata, ListView2);
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