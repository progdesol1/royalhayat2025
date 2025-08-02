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
using Database;

namespace Web.Master
{
    public partial class Appointment : System.Web.UI.Page
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
                //FirstData();
                btnAdd.ValidationGroup = "ss";
            }
        }
        #region Step2
        public void BindData()
        {
            List<Database.Appointment> List = DB.Appointments.Where(p=>p.TenentID == TID && p.Deleted == true).ToList();
            Listview1.DataSource = List;
            Listview1.DataBind();
            //int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
            //int Totalrec = List.Count();
            //((AcmMaster)Page.Master).Loadlist(Showdata, take, Skip, ChoiceID, lblShowinfEntry, btnPrevious1, btnNext1, Listview1, ListView2, Totalrec, List);
        }
        #endregion

        public void GetShow()
        {

            lblTitle1s.Attributes["class"] = lblStartDt1s.Attributes["class"] = lblEndDt1s.Attributes["class"] = lblColor1s.Attributes["class"] = lblurl1s.Attributes["class"] = lblActive1s.Attributes["class"] = lblDeleted1s.Attributes["class"] = "control-label col-md-4  getshow";
            lblTitle2h.Attributes["class"] = lblStartDt2h.Attributes["class"] = lblEndDt2h.Attributes["class"] = lblColor2h.Attributes["class"] = lblurl2h.Attributes["class"] = lblActive2h.Attributes["class"] = lblDeleted2h.Attributes["class"] = "control-label col-md-4  gethide";
            b.Attributes.Remove("dir");
            b.Attributes.Add("dir", "ltr");

        }

        public void GetHide()
        {
            lblTitle1s.Attributes["class"] = lblStartDt1s.Attributes["class"] = lblEndDt1s.Attributes["class"] = lblColor1s.Attributes["class"] = lblurl1s.Attributes["class"] = lblActive1s.Attributes["class"] = lblDeleted1s.Attributes["class"] = "control-label col-md-4  gethide";
            lblTitle2h.Attributes["class"] = lblStartDt2h.Attributes["class"] = lblEndDt2h.Attributes["class"] = lblColor2h.Attributes["class"] = lblurl2h.Attributes["class"] = lblActive2h.Attributes["class"] = lblDeleted2h.Attributes["class"] = "control-label col-md-4  getshow";
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
            txtStartDt.Text = "";
            txtEndDt.Text = "";
            drpColor.SelectedIndex = 0;
            txturl.Text = "";
            //drpCreateby.SelectedIndex = 0;
            //txtDateTime.Text = "";
            cbActive.Text = "";
            cbDeleted.Text = "";
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
                        btnAdd.Text = "Add";
                        btnAdd.ValidationGroup = "submit";
                    }
                    else if (btnAdd.Text == "Add")
                    {
                        //Database.Appointment objAppointment = new Database.Appointment();
                        ////Server Content Send data Yogesh
                        //objAppointment.TenentID = TID;
                        //objAppointment.LocationID = 1;
                        //objAppointment.Title = txtTitle.Text;
                        //objAppointment.StartDt = Convert.ToDateTime(txtStartDt.Text);
                        //objAppointment.EndDt = Convert.ToDateTime(txtEndDt.Text);
                        //objAppointment.Color = drpColor.SelectedValue;
                        //objAppointment.url = txturl.Text;
                        //objAppointment.Createby = 1;
                        //objAppointment.DateTime = DateTime.Now;
                        //objAppointment.Active = cbActive.Checked == true ? true : false;
                        //objAppointment.Deleted = cbDeleted.Checked == true ? true : false;

                        int ID = 0;
                        string Tilte = txtTitle.Text;
                        DateTime StartDt = Convert.ToDateTime(txtStartDt.Text);
                        DateTime EndDt = Convert.ToDateTime(txtEndDt.Text);
                        string Color = drpColor.SelectedValue;
                        string url = txturl.Text;
                        bool Active = cbActive.Checked == true ? true : false;
                        bool Deleted = cbDeleted.Checked == true ? true : false;
                        string CRMref = "";
                        string Status = "Insert";
                        Classes.CRMClass.InsertAppointment(ID, TID, 1, Tilte, StartDt, EndDt, Color, url, Active, Deleted, null, Status);

                        //DB.Appointments.AddObject(objAppointment);
                        //DB.SaveChanges();
                        Clear();
                        btnAdd.ValidationGroup = "ss";
                        btnAdd.Text = "Add New";
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
                            //Database.Appointment objAppointment = DB.Appointments.Single(p => p.TenentID == TID && p.ID == ID);
                            
                            //objAppointment.Title = txtTitle.Text;
                            //objAppointment.StartDt = Convert.ToDateTime(txtStartDt.Text);
                            //objAppointment.EndDt = Convert.ToDateTime(txtEndDt.Text);
                            //objAppointment.Color = drpColor.SelectedValue;
                            //objAppointment.url = txturl.Text;
                            //objAppointment.Active = cbActive.Checked == true ? true : false;
                            //objAppointment.Deleted = cbDeleted.Checked == true ? true : false;

                            int IDD = ID;
                            string Tilte = txtTitle.Text;
                            DateTime StartDt = Convert.ToDateTime(txtStartDt.Text);
                            DateTime EndDt = Convert.ToDateTime(txtEndDt.Text);
                            string Color = drpColor.SelectedValue;
                            string url = txturl.Text;
                            bool Active = cbActive.Checked == true ? true : false;
                            bool Deleted = cbDeleted.Checked == true ? true : false;
                            string CRMref = "";
                            string Status = "Update";
                            Classes.CRMClass.InsertAppointment(IDD, TID, 1, Tilte, StartDt, EndDt, Color, url, Active, Deleted, null, Status);


                            ViewState["Edit"] = null;
                            btnAdd.ValidationGroup = "ss";
                            btnAdd.Text = "Add New";
                            //DB.SaveChanges();
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
            //drpDeleted.Items.Insert(0, new ListItem("-- Select --", "0"));drpDeleted.DataSource = DB.0;drpDeleted.DataTextField = "0";drpDeleted.DataValueField = "0";drpDeleted.DataBind();
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
            //drpTenentID.SelectedValue = Listview1.SelectedDataKey[0].ToString();
            //drpLocationID.SelectedValue = Listview1.SelectedDataKey[0].ToString();
            txtTitle.Text = Listview1.SelectedDataKey[0].ToString();
            txtStartDt.Text = Listview1.SelectedDataKey[0].ToString();
            txtEndDt.Text = Listview1.SelectedDataKey[0].ToString();
            //txtColor.Text = Listview1.SelectedDataKey[0].ToString();
            txturl.Text = Listview1.SelectedDataKey[0].ToString();
            //drpCreateby.SelectedValue = Listview1.SelectedDataKey[0].ToString();
            //txtDateTime.Text = Listview1.SelectedDataKey[0].ToString();
            //cbActive.Checked = Listview1.SelectedDataKey[0].ToString();
            //cbDeleted.Checked = Listview1.SelectedDataKey[0].ToString();

        }
        public void NextData()
        {

            if (Listview1.SelectedIndex != Listview1.Items.Count - 1)
            {
                Listview1.SelectedIndex = Listview1.SelectedIndex + 1;
                //drpTenentID.SelectedValue = Listview1.SelectedDataKey[0].ToString();
                //drpLocationID.SelectedValue = Listview1.SelectedDataKey[0].ToString();
                txtTitle.Text = Listview1.SelectedDataKey[0].ToString();
                txtStartDt.Text = Listview1.SelectedDataKey[0].ToString();
                txtEndDt.Text = Listview1.SelectedDataKey[0].ToString();
                //txtColor.Text = Listview1.SelectedDataKey[0].ToString();
                //txturl.Text = Listview1.SelectedDataKey[0].ToString();
                //drpCreateby.SelectedValue = Listview1.SelectedDataKey[0].ToString();
                //txtDateTime.Text = Listview1.SelectedDataKey[0].ToString();
                //cbActive.Checked = Listview1.SelectedDataKey[0].ToString();
                //cbDeleted.Checked = Listview1.SelectedDataKey[0].ToString();

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
                //drpTenentID.SelectedValue = Listview1.SelectedDataKey[0].ToString();
                //drpLocationID.SelectedValue = Listview1.SelectedDataKey[0].ToString();
                //txtTitle.Text = Listview1.SelectedDataKey[0].ToString();
                //txtStartDt.Text = Listview1.SelectedDataKey[0].ToString();
                //txtEndDt.Text = Listview1.SelectedDataKey[0].ToString();
                //txtColor.Text = Listview1.SelectedDataKey[0].ToString();
                //txturl.Text = Listview1.SelectedDataKey[0].ToString();
                //drpCreateby.SelectedValue = Listview1.SelectedDataKey[0].ToString();
                //txtDateTime.Text = Listview1.SelectedDataKey[0].ToString();
                //cbActive.Checked = Listview1.SelectedDataKey[0].ToString();
                //cbDeleted.Checked = Listview1.SelectedDataKey[0].ToString();

            }
        }
        public void LastData()
        {
            Listview1.SelectedIndex = Listview1.Items.Count - 1;
            //drpTenentID.SelectedValue = Listview1.SelectedDataKey[0].ToString();
            //drpLocationID.SelectedValue = Listview1.SelectedDataKey[0].ToString();
            //txtTitle.Text = Listview1.SelectedDataKey[0].ToString();
            //txtStartDt.Text = Listview1.SelectedDataKey[0].ToString();
            //txtEndDt.Text = Listview1.SelectedDataKey[0].ToString();
            //txtColor.Text = Listview1.SelectedDataKey[0].ToString();
            //txturl.Text = Listview1.SelectedDataKey[0].ToString();
            //drpCreateby.SelectedValue = Listview1.SelectedDataKey[0].ToString();
            //txtDateTime.Text = Listview1.SelectedDataKey[0].ToString();
            //cbActive.Checked = Listview1.SelectedDataKey[0].ToString();
            //cbDeleted.Checked = Listview1.SelectedDataKey[0].ToString();

        }


        protected void btnEditLable_Click(object sender, EventArgs e)
        {
            if (Session["LANGUAGE"].ToString() == "ar-KW")
            {
                if (btnEditLable.Text == "Update Label")
                {

                    //2false
                    lblTitle2h.Visible = lblStartDt2h.Visible = lblEndDt2h.Visible = lblColor2h.Visible = lblurl2h.Visible = lblActive2h.Visible = lblDeleted2h.Visible = false;
                    //2true
                    txtTitle2h.Visible = txtStartDt2h.Visible = txtEndDt2h.Visible = txtColor2h.Visible = txturl2h.Visible = txtActive2h.Visible = txtDeleted2h.Visible = true;

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
                    lblTitle2h.Visible = lblStartDt2h.Visible = lblEndDt2h.Visible = lblColor2h.Visible = lblurl2h.Visible = lblActive2h.Visible = lblDeleted2h.Visible = true;
                    //2false
                    txtTitle2h.Visible = txtStartDt2h.Visible = txtEndDt2h.Visible = txtColor2h.Visible = txturl2h.Visible = txtActive2h.Visible = txtDeleted2h.Visible = false;

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
                    lblTitle1s.Visible = lblStartDt1s.Visible = lblEndDt1s.Visible = lblColor1s.Visible = lblurl1s.Visible = lblActive1s.Visible = lblDeleted1s.Visible = false;
                    //1true
                    txtTitle1s.Visible = txtStartDt1s.Visible = txtEndDt1s.Visible = txtColor1s.Visible = txturl1s.Visible = txtActive1s.Visible = txtDeleted1s.Visible = true;
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
                    lblTitle1s.Visible = lblStartDt1s.Visible = lblEndDt1s.Visible = lblColor1s.Visible = lblurl1s.Visible = lblActive1s.Visible = lblDeleted1s.Visible = true;
                    //1false
                    txtTitle1s.Visible = txtStartDt1s.Visible = txtEndDt1s.Visible = txtColor1s.Visible = txturl1s.Visible = txtActive1s.Visible = txtDeleted1s.Visible = false;
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

            List<Database.TBLLabelDTL> List = ((AcmMaster)this.Master).Bindxml("Appointment").Where(p => p.LabelMstID == PID && p.LANGDISP == lang).ToList();
            foreach (Database.TBLLabelDTL item in List)
            {
                if (lblTitle1s.ID == item.LabelID)
                    txtTitle1s.Text = lblTitle1s.Text = lblhTitle.Text = item.LabelName;
                else if (lblStartDt1s.ID == item.LabelID)
                    txtStartDt1s.Text = lblStartDt1s.Text = lblhStartDt.Text = item.LabelName;
                else if (lblEndDt1s.ID == item.LabelID)
                    txtEndDt1s.Text = lblEndDt1s.Text = lblhEndDt.Text = item.LabelName;
                else if (lblColor1s.ID == item.LabelID)
                    txtColor1s.Text = lblColor1s.Text = lblhColor.Text = item.LabelName;
                else if (lblurl1s.ID == item.LabelID)
                    txturl1s.Text = lblurl1s.Text = lblhurl.Text = item.LabelName;
                else if (lblActive1s.ID == item.LabelID)
                    txtActive1s.Text = lblActive1s.Text = item.LabelName;
                else if (lblDeleted1s.ID == item.LabelID)
                    txtDeleted1s.Text = lblDeleted1s.Text = item.LabelName;

                else if (lblTitle2h.ID == item.LabelID)
                    txtTitle2h.Text = lblTitle2h.Text = lblhTitle.Text = item.LabelName;
                else if (lblStartDt2h.ID == item.LabelID)
                    txtStartDt2h.Text = lblStartDt2h.Text = lblhStartDt.Text = item.LabelName;
                else if (lblEndDt2h.ID == item.LabelID)
                    txtEndDt2h.Text = lblEndDt2h.Text = lblhEndDt.Text = item.LabelName;
                else if (lblColor2h.ID == item.LabelID)
                    txtColor2h.Text = lblColor2h.Text = lblhColor.Text = item.LabelName;
                else if (lblurl2h.ID == item.LabelID)
                    txturl2h.Text = lblurl2h.Text = lblhurl.Text = item.LabelName;
                else if (lblActive2h.ID == item.LabelID)
                    txtActive2h.Text = lblActive2h.Text = item.LabelName;
                else if (lblDeleted2h.ID == item.LabelID)
                    txtDeleted2h.Text = lblDeleted2h.Text = item.LabelName;

                else
                    txtHeader.Text = lblHeader.Text = Label5.Text = item.LabelName;
            }

        }
        public void SaveLabel(string lang)
        {
            string PID = ((AcmMaster)this.Master).getOwnPage();
            //List<Database.TBLLabelDTL> List = DB.TBLLabelDTLs.Where(p => p.LabelMstID == PID  && p.LANGDISP == lang).ToList();
            List<Database.TBLLabelDTL> List = ((AcmMaster)this.Master).Bindxml("Appointment").Where(p => p.LabelMstID == PID && p.LANGDISP == lang).ToList();
            DataSet ds = new DataSet();
            ds.ReadXml(Server.MapPath("\\Master\\xml\\Appointment.xml"));
            foreach (Database.TBLLabelDTL item in List)
            {

                var obj = ((AcmMaster)this.Master).Bindxml("Appointment").Single(p => p.LabelID == item.LabelID && p.LabelMstID == PID && p.LANGDISP == lang);
                int i = obj.ID - 1;

                if (lblTitle1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtTitle1s.Text;
                else if (lblStartDt1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtStartDt1s.Text;
                else if (lblEndDt1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtEndDt1s.Text;
                else if (lblColor1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtColor1s.Text;
                else if (lblurl1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txturl1s.Text;
                else if (lblActive1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtActive1s.Text;
                else if (lblDeleted1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtDeleted1s.Text;

                else if (lblTitle2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtTitle2h.Text;
                else if (lblStartDt2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtStartDt2h.Text;
                else if (lblEndDt2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtEndDt2h.Text;
                else if (lblColor2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtColor2h.Text;
                else if (lblurl2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txturl2h.Text;
                else if (lblActive2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtActive2h.Text;
                else if (lblDeleted2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtDeleted2h.Text;

                else
                    ds.Tables[0].Rows[i]["LabelName"] = txtHeader.Text;
            }
            ds.WriteXml(Server.MapPath("\\Master\\xml\\Appointment.xml"));

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
            //drpTenentID.Enabled = true;
            //drpLocationID.Enabled = true;
            txtTitle.Enabled = true;
            txtStartDt.Enabled = true;
            txtEndDt.Enabled = true;
            drpColor.Enabled = true;
            txturl.Enabled = true;            
            cbActive.Enabled = true;
            cbDeleted.Enabled = true;

        }
        public void Readonly()
        {           
            txtTitle.Enabled = false;
            txtStartDt.Enabled = false;
            txtEndDt.Enabled = false;
            drpColor.Enabled = false;
            txturl.Enabled = false;
            //drpCreateby.Enabled = false;
            //txtDateTime.Enabled = false;
            cbActive.Enabled = false;
            cbDeleted.Enabled = false;
        }

        #region Listview
        //protected void btnNext1_Click(object sender, EventArgs e)
        //{

        //    int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
        //    int Totalrec = DB.Appointments.Count();
        //    if (ViewState["Take"] == null && ViewState["Skip"] == null)
        //    {
        //        ViewState["Take"] = Showdata;
        //        ViewState["Skip"] = 0;
        //    }
        //    take = Convert.ToInt32(ViewState["Take"]);
        //    take = take + Showdata;
        //    Skip = take - Showdata;

        //    ((AcmMaster)Page.Master).BindList(Listview1, (DB.Appointments.OrderBy(m => m.ID).Take(take).Skip(Skip)).ToList());
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
        //        int Totalrec = DB.Appointments.Count();
        //        Skip = Convert.ToInt32(ViewState["Skip"]);
        //        take = Skip;
        //        Skip = take - Showdata;
        //        ((AcmMaster)Page.Master).BindList(Listview1, (DB.Appointments.OrderBy(m => m.ID).Take(take).Skip(Skip)).ToList());
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
        //        int Totalrec = DB.Appointments.Count();
        //        take = Showdata;
        //        Skip = 0;
        //        ((AcmMaster)Page.Master).BindList(Listview1, (DB.Appointments.OrderBy(m => m.ID).Take(take).Skip(Skip)).ToList());
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
        //    int Totalrec = DB.Appointments.Count();
        //    take = Totalrec;
        //    Skip = Totalrec - Showdata;
        //    ((AcmMaster)Page.Master).BindList(Listview1, (DB.Appointments.OrderBy(m => m.ID).Take(take).Skip(Skip)).ToList());
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
                        //string[] ID = e.CommandArgument.ToString().Split(',');
                        //string str1 = ID[0].ToString();
                        //string str2 = ID[1].ToString();
                        int ID = Convert.ToInt32(e.CommandArgument);

                        Database.Appointment objSOJobDesc = DB.Appointments.Single(p =>p.TenentID == TID && p.ID == ID);
                        objSOJobDesc.Deleted = false;
                        DB.SaveChanges();
                        BindData();                       
                    }
                    if (e.CommandName == "btnEdit")
                    {
                        //string[] ID = e.CommandArgument.ToString().Split(',');
                        //string str1 = ID[0].ToString();
                        //string str2 = ID[1].ToString();
                        int ID = Convert.ToInt32(e.CommandArgument);

                        Database.Appointment objAppointment = DB.Appointments.Single(p => p.TenentID == TID && p.ID == ID);
                        //drpTenentID.SelectedValue = objAppointment.TenentID.ToString();
                        //drpLocationID.SelectedValue = objAppointment.LocationID.ToString();
                        txtTitle.Text = objAppointment.Title.ToString();
                        txtStartDt.Text = Convert.ToDateTime(objAppointment.StartDt).ToShortDateString();
                        txtEndDt.Text = Convert.ToDateTime(objAppointment.EndDt).ToShortDateString();
                        drpColor.SelectedValue = objAppointment.Color.ToString();
                        txturl.Text = objAppointment.url.ToString();                        
                        cbActive.Checked = (objAppointment.Active == true) ? true : false;
                        cbDeleted.Checked = (objAppointment.Deleted == true) ? true : false;

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

        //protected void ListView2_ItemCommand(object sender, ListViewCommandEventArgs e)
        //{

        //    int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
        //    int Totalrec = DB.Appointments.Count();
        //    if (e.CommandName == "LinkPageavigation")
        //    {
        //        int ID = Convert.ToInt32(e.CommandArgument);
        //        ViewState["Take"] = ID * Showdata;
        //        ViewState["Skip"] = (ID * Showdata) - Showdata;
        //        int Tvalue = Convert.ToInt32(ViewState["Take"]);
        //        int Svalue = Convert.ToInt32(ViewState["Skip"]);
        //        ((AcmMaster)Page.Master).BindList(Listview1, (DB.Appointments.OrderBy(m => m.ID).Take(Tvalue).Skip(Svalue)).ToList());
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
        public string GetColor(int id)
        {
            if (id == 1)
                return "Red";
            else if (id == 2)
                return "Green";
            else if (id == 3)
                return "Blue";
            else if (id == 4)
                return "Yellow";
            else if (id == 5)
                return "Purple";
            else
                return "Not Found";            
        }
        
    }
}