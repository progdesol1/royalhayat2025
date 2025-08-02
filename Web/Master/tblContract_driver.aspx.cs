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
    public partial class tblContract_driver : System.Web.UI.Page
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
                //FirstData();
                btnAdd.ValidationGroup = "s";

            }
        }
        #region Step2
        public void BindData()
        {
            int TID = (((USER_MST)Session["USER"]).TenentID);
            Listview1.DataSource = DB.tblContract_driver.Where(p => p.TenentID == TID && p.active == true);
            Listview1.DataBind();
            //List<tblContract_driver> List = DB.tblContract_driver.Where(p=>p.TenentID==TID).OrderBy(m => m.contactId).ToList();
            //int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
            //int Totalrec = List.Count();
            //((AcmMaster)Page.Master).Loadlist(Showdata, take, Skip, ChoiceID, lblShowinfEntry, btnPrevious1, btnNext1, Listview1, ListView2, Totalrec, List);
        }
        #endregion

        public void GetShow()
        {

            lblcontactId1s.Attributes["class"] = lbltimedelivered1s.Attributes["class"] = "control-label col-md-4  getshow";//lbllocationid1s.Attributes["class"] = lblinvoiceid1s.Attributes["class"] = lbltripDate1s.Attributes["class"] = lbltripid1s.Attributes["class"] = lbldifferentials1s.Attributes["class"] =
            lblcontactId2h.Attributes["class"] = lbltimedelivered2h.Attributes["class"] = "control-label col-md-4  gethide"; //lbldifferentials2h.Attributes["class"] = lbllocationid2h.Attributes["class"] =lblinvoiceid2h.Attributes["class"] = lbltripDate2h.Attributes["class"] = lbltripid2h.Attributes["class"] =
            b.Attributes.Remove("dir");
            b.Attributes.Add("dir", "ltr");

        }

        public void GetHide()
        {
            lblcontactId1s.Attributes["class"] = lbltimedelivered1s.Attributes["class"] = "control-label col-md-4  gethide";// lbllocationid1s.Attributes["class"] =lblinvoiceid1s.Attributes["class"] = lbltripDate1s.Attributes["class"] = lbltripid1s.Attributes["class"] =lbldifferentials1s.Attributes["class"] =
            lblcontactId2h.Attributes["class"] = lbltimedelivered2h.Attributes["class"] = "control-label col-md-4  getshow";//lbldifferentials2h.Attributes["class"] =lblinvoiceid2h.Attributes["class"] = lbltripDate2h.Attributes["class"] = lbltripid2h.Attributes["class"] =lbllocationid2h.Attributes["class"] =
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
            //drplocationid.SelectedIndex = 0;
            drpcontactId.SelectedIndex = 0;
            //drpinvoiceid.SelectedIndex = 0;
            //txttripDate.Text = "";
            //drptripid.SelectedIndex = 0;
            txttimedelivered.Text = "";
            txtName.Text = "";
            ////drpfinalstatus.SelectedIndex =0 ;
            //txtdifferentials.Text = "";
            //drpcrupid.SelectedIndex = 0;
            //drpactive.SelectedIndex = 0;

        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            int TID = (((USER_MST)Session["USER"]).TenentID);
            //using (TransactionScope scope = new TransactionScope())
            //{
            //    try
            //    {
                    if (btnAdd.Text == "AddNew")
                    {

                        Write();
                        Clear();
                        btnAdd.Text = "Add";
                        btnAdd.ValidationGroup = "submit";
                    }
                    else if (btnAdd.Text == "Add")
                    {
                        Database.tblContract_driver objtblContract_driver = new Database.tblContract_driver();
                        //Server Content Send data Yogesh
                        objtblContract_driver.TenentID = TID;
                        objtblContract_driver.locationid = 1;
                        //objtblContract_driver.locationid = Convert.ToInt32(drplocationid.SelectedValue);
                        objtblContract_driver.contactId = Convert.ToInt32(drpcontactId.SelectedValue);
                        //objtblContract_driver.invoiceid = Convert.ToInt32(drpinvoiceid.SelectedValue);
                        //objtblContract_driver.tripDate = Convert.ToDateTime(txttripDate.Text);
                        //objtblContract_driver.tripid = Convert.ToInt32(drptripid.SelectedValue);
                        objtblContract_driver.timedelivered = Convert.ToDateTime(txttimedelivered.Text);
                        objtblContract_driver.active = true;
                        //objtblContract_driver.differentials = txtdifferentials.Text;
                        //objtblContract_driver.crupid = Convert.ToInt32(drpcrupid.SelectedValue);
                        //objtblContract_driver.active = Convert.ToInt32(drpactive.SelectedValue);
                        


                        DB.tblContract_driver.AddObject(objtblContract_driver);
                        DB.SaveChanges();
                        Clear();
                        btnAdd.Text = "AddNew";
                        lblMsg.Text = "  Data Save Successfully";
                        pnlSuccessMsg.Visible = true;
                        BindData();
                        //navigation.Visible = true;
                        Readonly();
                        //FirstData();
                        btnAdd.ValidationGroup = "s";
                    }
                    else if (btnAdd.Text == "Update")
                    {

                        if (ViewState["Edit"] != null)
                        {
                            int ID = Convert.ToInt32(ViewState["Edit"]);
                            Database.tblContract_driver objtblContract_driver = DB.tblContract_driver.Single(p => p.contactId == ID && p.TenentID==TID);
                            //objtblContract_driver.locationid = Convert.ToInt32(drplocationid.SelectedValue);
                            objtblContract_driver.contactId = Convert.ToInt32(drpcontactId.SelectedValue);
                            //objtblContract_driver.invoiceid = Convert.ToInt32(drpinvoiceid.SelectedValue);
                            //objtblContract_driver.tripDate = Convert.ToDateTime(txttripDate.Text);
                            //objtblContract_driver.tripid = Convert.ToInt32(drptripid.SelectedValue);
                            objtblContract_driver.timedelivered = Convert.ToDateTime(txttimedelivered.Text);
                           
                            //objtblContract_driver.differentials = txtdifferentials.Text;
                            //objtblContract_driver.crupid = Convert.ToInt32(drpcrupid.SelectedValue);
                            //objtblContract_driver.active = Convert.ToInt32(drpactive.SelectedValue);

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
                            btnAdd.ValidationGroup = "s";
                        }
                    }
                    BindData();

            //        scope.Complete(); //  To commit.

            //    }
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
        public string getname(int ID)
        {
            int TID = (((USER_MST)Session["USER"]).TenentID);
            return DB.TBLCONTACTs.Single(p => p.CONTACTID == ID && p.TenentID == TID).PersName1;
        }

        public void FillContractorID()
        {
            int TID = (((USER_MST)Session["USER"]).TenentID);
            
            drpcontactId.DataSource = DB.TBLCONTACTs.Where(p=>p.TenentID==TID);
            drpcontactId.DataTextField = "PersName1";
            drpcontactId.DataValueField = "CONTACTID";
            drpcontactId.DataBind();
            drpcontactId.Items.Insert(0, new ListItem("-- Select --", "0"));


            //drpInvoiceID.DataSource = DB.ICTR_HD.Where(p => p.TenentID == TID);
            //drpInvoiceID.DataTextField = "InvoiceNO";
            //drpInvoiceID.DataValueField = "MYTRANSID";
            //drpInvoiceID.DataBind();
            //drpInvoiceID.Items.Insert(0, new ListItem("-- Select --", "0"));
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
            //drplocationid.SelectedValue = Listview1.SelectedDataKey[0].ToString();
            drpcontactId.SelectedValue = Listview1.SelectedDataKey[0].ToString();
            //drpinvoiceid.SelectedValue = Listview1.SelectedDataKey[0].ToString();
            //txttripDate.Text = Listview1.SelectedDataKey[0].ToString();
            //drptripid.SelectedValue = Listview1.SelectedDataKey[0].ToString();
            txttimedelivered.Text = Listview1.SelectedDataKey[0].ToString();
           
            //txtdifferentials.Text = Listview1.SelectedDataKey[0].ToString();
            //drpcrupid.SelectedValue = Listview1.SelectedDataKey[0].ToString();
            //drpactive.SelectedValue = Listview1.SelectedDataKey[0].ToString();

        }
        public void NextData()
        {

            if (Listview1.SelectedIndex != Listview1.Items.Count - 1)
            {
                Listview1.SelectedIndex = Listview1.SelectedIndex + 1;
                //drplocationid.SelectedValue = Listview1.SelectedDataKey[0].ToString();
                drpcontactId.SelectedValue = Listview1.SelectedDataKey[0].ToString();
                //drpinvoiceid.SelectedValue = Listview1.SelectedDataKey[0].ToString();
                //txttripDate.Text = Listview1.SelectedDataKey[0].ToString();
                //drptripid.SelectedValue = Listview1.SelectedDataKey[0].ToString();
                txttimedelivered.Text = Listview1.SelectedDataKey[0].ToString();
                
               // txtdifferentials.Text = Listview1.SelectedDataKey[0].ToString();
                //drpcrupid.SelectedValue = Listview1.SelectedDataKey[0].ToString();
                //drpactive.SelectedValue = Listview1.SelectedDataKey[0].ToString();

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
                //drplocationid.SelectedValue = Listview1.SelectedDataKey[0].ToString();
                drpcontactId.SelectedValue = Listview1.SelectedDataKey[0].ToString();
                //drpinvoiceid.SelectedValue = Listview1.SelectedDataKey[0].ToString();
                //txttripDate.Text = Listview1.SelectedDataKey[0].ToString();
                //drptripid.SelectedValue = Listview1.SelectedDataKey[0].ToString();
                txttimedelivered.Text = Listview1.SelectedDataKey[0].ToString();
              
                //txtdifferentials.Text = Listview1.SelectedDataKey[0].ToString();
                //drpcrupid.SelectedValue = Listview1.SelectedDataKey[0].ToString();
                //drpactive.SelectedValue = Listview1.SelectedDataKey[0].ToString();

            }
        }
        public void LastData()
        {
            Listview1.SelectedIndex = Listview1.Items.Count - 1;
            //drplocationid.SelectedValue = Listview1.SelectedDataKey[0].ToString();
            drpcontactId.SelectedValue = Listview1.SelectedDataKey[0].ToString();
            //drpinvoiceid.SelectedValue = Listview1.SelectedDataKey[0].ToString();
            //txttripDate.Text = Listview1.SelectedDataKey[0].ToString();
            //drptripid.SelectedValue = Listview1.SelectedDataKey[0].ToString();
            txttimedelivered.Text = Listview1.SelectedDataKey[0].ToString();
           
            //txtdifferentials.Text = Listview1.SelectedDataKey[0].ToString();
            //drpcrupid.SelectedValue = Listview1.SelectedDataKey[0].ToString();
            //drpactive.SelectedValue = Listview1.SelectedDataKey[0].ToString();

        }


        protected void btnEditLable_Click(object sender, EventArgs e)
        {
            if (Session["LANGUAGE"].ToString() == "ar-KW")
            {
                if (btnEditLable.Text == "Update Label")
                {

                    //2false
                    lblcontactId2h.Visible = lbltimedelivered2h.Visible = false; //lbllocationid2h.Visible =lblinvoiceid2h.Visible = lbltripDate2h.Visible = lbltripid2h.Visible =lbldifferentials2h.Visible 
                    //2true
                    txtcontactId2h.Visible = txttimedelivered2h.Visible = true;//txtinvoiceid2h.Visible = txttripDate2h.Visible = txttripid2h.Visible = txtlocationid2h.Visible = txtdifferentials2h.Visible 

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
                    lblcontactId2h.Visible = lbltimedelivered2h.Visible = true;//lblinvoiceid2h.Visible = lbltripDate2h.Visible = lbltripid2h.Visible =lbldifferentials2h.Visible= lbllocationid2h.Visible 
                    //2false
                    txtcontactId2h.Visible = txttimedelivered2h.Visible = false;//txtdifferentials2h.Visible =txtinvoiceid2h.Visible = txttripDate2h.Visible = txttripid2h.Visible = txtlocationid2h.Visible 

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
                    lblcontactId1s.Visible = lbltimedelivered1s.Visible = false;//lblinvoiceid1s.Visible = lbltripDate1s.Visible = lbltripid1s.Visible = lbldifferentials1s.Visible = lbllocationid1s.Visible =
                    //1true
                    txtcontactId1s.Visible = txttimedelivered1s.Visible = true;// txtinvoiceid1s.Visible = txttripDate1s.Visible = txttripid1s.Visible =  txtdifferentials1s.Visible =txtlocationid1s.Visible =
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
                    lblcontactId1s.Visible = lbltimedelivered1s.Visible = true;// lbllocationid1s.Visible =lbldifferentials1s.Visible =lblinvoiceid1s.Visible = lbltripDate1s.Visible = lbltripid1s.Visible =
                    //1false
                    txtcontactId1s.Visible = txttimedelivered1s.Visible = false;//txtdifferentials1s.Visible =  txtlocationid1s.Visible =txtinvoiceid1s.Visible = txttripDate1s.Visible = txttripid1s.Visible =
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

            List<Database.TBLLabelDTL> List = ((AcmMaster)this.Master).Bindxml("tblContract_driver").Where(p => p.LabelMstID == PID && p.LANGDISP == lang).ToList();
            foreach (Database.TBLLabelDTL item in List)
            {
                //if (lbllocationid1s.ID == item.LabelID)
                //    txtlocationid1s.Text = lbllocationid1s.Text = item.LabelName;
                 if (lblcontactId1s.ID == item.LabelID)
                    txtcontactId1s.Text = lblcontactId1s.Text = item.LabelName;
                //else if (lblinvoiceid1s.ID == item.LabelID)
                //    txtinvoiceid1s.Text = lblinvoiceid1s.Text  = item.LabelName;
                //else if (lbltripDate1s.ID == item.LabelID)
                //    txttripDate1s.Text = lbltripDate1s.Text = item.LabelName;
                //else if (lbltripid1s.ID == item.LabelID)
                //    txttripid1s.Text = lbltripid1s.Text = item.LabelName;
                 else if (lbltimedelivered1s.ID == item.LabelID)
                     txttimedelivered1s.Text = lbltimedelivered1s.Text = item.LabelName;
                //else if (lbldifferentials1s.ID == item.LabelID)
                //    txtdifferentials1s.Text = lbldifferentials1s.Text = item.LabelName;

               //else if (lbllocationid2h.ID == item.LabelID)
               //     txtlocationid2h.Text = lbllocationid2h.Text  = item.LabelName;
               // else if (lblcontactId2h.ID == item.LabelID)
               //     txtcontactId2h.Text = lblcontactId2h.Text= item.LabelName;
               // else if (lblinvoiceid2h.ID == item.LabelID)
               //     txtinvoiceid2h.Text = lblinvoiceid2h.Text  = item.LabelName;
               // else if (lbltripDate2h.ID == item.LabelID)
               //     txttripDate2h.Text = lbltripDate2h.Text =item.LabelName;
               // else if (lbltripid2h.ID == item.LabelID)
               //     txttripid2h.Text = lbltripid2h.Text = item.LabelName;
                else if (lbltimedelivered2h.ID == item.LabelID)
                    txttimedelivered2h.Text = lbltimedelivered2h.Text = item.LabelName;
                //else if (lbldifferentials2h.ID == item.LabelID)
                //    txtdifferentials2h.Text = lbldifferentials2h.Text  = item.LabelName;

                else
                    txtHeader.Text = lblHeader.Text = Label5.Text = item.LabelName;
            }

        }
        public void SaveLabel(string lang)
        {
            string PID = ((AcmMaster)this.Master).getOwnPage();
            //List<Database.TBLLabelDTL> List = DB.TBLLabelDTLs.Where(p => p.LabelMstID == PID  && p.LANGDISP == lang).ToList();
            List<Database.TBLLabelDTL> List = ((AcmMaster)this.Master).Bindxml("tblContract_driver").Where(p => p.LabelMstID == PID && p.LANGDISP == lang).ToList();
            DataSet ds = new DataSet();
            ds.ReadXml(Server.MapPath("\\Master\\xml\\tblContract_driver.xml"));
            foreach (Database.TBLLabelDTL item in List)
            {

                var obj = ((AcmMaster)this.Master).Bindxml("tblContract_driver").Single(p => p.LabelID == item.LabelID && p.LabelMstID == PID && p.LANGDISP == lang);
                int i = obj.ID - 1;

                //if (lbllocationid1s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtlocationid1s.Text;
                 if (lblcontactId1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtcontactId1s.Text;
                //else if (lblinvoiceid1s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtinvoiceid1s.Text;
                //else if (lbltripDate1s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txttripDate1s.Text;
                //else if (lbltripid1s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txttripid1s.Text;
                 else if (lbltimedelivered1s.ID == item.LabelID)
                     ds.Tables[0].Rows[i]["LabelName"] = txttimedelivered1s.Text;
                //else if (lbldifferentials1s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtdifferentials1s.Text;

                //if (lbllocationid2h.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtlocationid2h.Text;
                //else if (lblcontactId2h.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtcontactId2h.Text;
                //else if (lblinvoiceid2h.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtinvoiceid2h.Text;
                //else if (lbltripDate2h.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txttripDate2h.Text;
                //else if (lbltripid2h.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txttripid2h.Text;
                else if (lbltimedelivered2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txttimedelivered2h.Text;
                //else if (lbldifferentials2h.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtdifferentials2h.Text;

                else
                    ds.Tables[0].Rows[i]["LabelName"] = txtHeader.Text;
            }
            ds.WriteXml(Server.MapPath("\\Master\\xml\\tblContract_driver.xml"));

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
            //drplocationid.Enabled = true;
            drpcontactId.Enabled = true;
            //drpinvoiceid.Enabled = true;
            //txttripDate.Enabled = true;
            //drptripid.Enabled = true;
            txttimedelivered.Enabled = true;
            txtName.Enabled = true;
            //txtdifferentials.Enabled = true;
            //drpcrupid.Enabled = true;
            //drpactive.Enabled = true;

        }
        public void Readonly()
        {
            //navigation.Visible = true;
            //drplocationid.Enabled = false;
            drpcontactId.Enabled = false;
            //drpinvoiceid.Enabled = false;
            //txttripDate.Enabled = false;
            //drptripid.Enabled = false;
            txttimedelivered.Enabled = false;
            txtName.Enabled = false;
            //txtdifferentials.Enabled = false;
            //drpcrupid.Enabled = false;
            //drpactive.Enabled = false;


        }

        #region Listview
        protected void btnNext1_Click(object sender, EventArgs e)
        {
            int TID = (((USER_MST)Session["USER"]).TenentID);
            int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
            int Totalrec = DB.tblContract_driver.Count();
            if (ViewState["Take"] == null && ViewState["Skip"] == null)
            {
                ViewState["Take"] = Showdata;
                ViewState["Skip"] = 0;
            }
            take = Convert.ToInt32(ViewState["Take"]);
            take = take + Showdata;
            Skip = take - Showdata;

            ((AcmMaster)Page.Master).BindList(Listview1, (DB.tblContract_driver.Where(p=>p.TenentID==TID).OrderBy(m => m.contactId).Take(take).Skip(Skip)).ToList());
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
            int TID = (((USER_MST)Session["USER"]).TenentID);
            int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
            if (ViewState["Take"] != null && ViewState["Skip"] != null)
            {
                int Totalrec = DB.tblContract_driver.Count();
                Skip = Convert.ToInt32(ViewState["Skip"]);
                take = Skip;
                Skip = take - Showdata;
                ((AcmMaster)Page.Master).BindList(Listview1, (DB.tblContract_driver.Where(p => p.TenentID == TID).OrderBy(m => m.contactId).Take(take).Skip(Skip)).ToList());
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
            int TID = (((USER_MST)Session["USER"]).TenentID);
            int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
            if (ViewState["Take"] != null && ViewState["Skip"] != null)
            {
                int Totalrec = DB.tblContract_driver.Count();
                take = Showdata;
                Skip = 0;
                ((AcmMaster)Page.Master).BindList(Listview1, (DB.tblContract_driver.Where(p => p.TenentID == TID).OrderBy(m => m.contactId).Take(take).Skip(Skip)).ToList());
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
            int TID = (((USER_MST)Session["USER"]).TenentID);
            int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
            int Totalrec = DB.tblContract_driver.Count();
            take = Totalrec;
            Skip = Totalrec - Showdata;
            ((AcmMaster)Page.Master).BindList(Listview1, (DB.tblContract_driver.Where(p => p.TenentID == TID).OrderBy(m => m.contactId).Take(take).Skip(Skip)).ToList());
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
             int TID = (((USER_MST)Session["USER"]).TenentID);
            //using (TransactionScope scope = new TransactionScope())
            //{
            //    try
            //    {
                    if (e.CommandName == "btnDelete")
                    {

                        //string[] ID = e.CommandArgument.ToString().Split(',');
                        //string str1 = ID[0].ToString();
                        //string str2 = ID[1].ToString();
                        int ID = Convert.ToInt32(e.CommandArgument);

                        Database.tblContract_driver objSOJobDesc = DB.tblContract_driver.Single(p => p.contactId == ID && p.TenentID == TID);
                        objSOJobDesc.active = false;
                        DB.SaveChanges();
                        BindData();
                        int Tvalue = Convert.ToInt32(ViewState["Take"]);
                        int Svalue = Convert.ToInt32(ViewState["Skip"]);
                        ((AcmMaster)Page.Master).BindList(Listview1, (DB.tblContract_driver.OrderBy(m => m.contactId).Take(Tvalue).Skip(Svalue)).ToList());

                    }

                    if (e.CommandName == "btnEdit")
                    {
                        //string[] ID = e.CommandArgument.ToString().Split(',');
                        //string str1 = ID[0].ToString();
                        //string str2 = ID[1].ToString();
                        int ID = Convert.ToInt32(e.CommandArgument);

                        Database.tblContract_driver objtblContract_driver = DB.tblContract_driver.Single(p => p.contactId == ID && p.TenentID == TID);
                        //drplocationid.SelectedValue = objtblContract_driver.locationid.ToString();
                        drpcontactId.SelectedValue = objtblContract_driver.contactId.ToString();
                        //drpinvoiceid.SelectedValue = objtblContract_driver.invoiceid.ToString();
                        //txttripDate.Text = objtblContract_driver.tripDate.ToString();
                        //drptripid.SelectedValue = objtblContract_driver.tripid.ToString();
                        txttimedelivered.Text = objtblContract_driver.timedelivered.ToString();
                        
                        //txtdifferentials.Text = objtblContract_driver.differentials.ToString();
                        //drpcrupid.SelectedValue = objtblContract_driver.crupid.ToString();
                        //drpactive.SelectedValue = objtblContract_driver.active.ToString();

                        btnAdd.Text = "Update";
                        ViewState["Edit"] = ID;
                        Write();
                    }
            //        scope.Complete(); //  To commit.
            //    }
            //    catch (Exception ex)
            //    {
            //        ScriptManager.RegisterClientScriptBlock(sender as Control, this.GetType(), "alert", ex.Message, true);
            //        throw;
            //    }
            //}
        }

        protected void ListView2_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            int TID = (((USER_MST)Session["USER"]).TenentID);
            int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
            int Totalrec = DB.tblContract_driver.Count();
            if (e.CommandName == "LinkPageavigation")
            {
                int ID = Convert.ToInt32(e.CommandArgument);
                ViewState["Take"] = ID * Showdata;
                ViewState["Skip"] = (ID * Showdata) - Showdata;
                int Tvalue = Convert.ToInt32(ViewState["Take"]);
                int Svalue = Convert.ToInt32(ViewState["Skip"]);
                ((AcmMaster)Page.Master).BindList(Listview1, (DB.tblContract_driver.Where(p=>p.TenentID==TID).OrderBy(m => m.contactId).Take(Tvalue).Skip(Svalue)).ToList());
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

        protected void drpcontactId_SelectedIndexChanged(object sender, EventArgs e)
        {
            int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            int LID = Convert.ToInt32(((USER_MST)Session["USER"]).LOCATION_ID);
            int CID=Convert.ToInt32(drpcontactId.SelectedValue);

            txtName.Text = DB.TBLCONTACTs.Single(p => p.TenentID == TID && p.CONTACTID == CID).PersName1;
            lblCname.Text = DB.TBLCONTACTs.Single(p => p.TenentID == TID && p.CONTACTID == CID).PersName1;
            lbldate.Text = DateTime.Now.ToShortDateString();
            if (DB.tblCOUNTRies.Where(p => p.TenentID == TID && p.COUNTRYID == CID).Count()>0)
            {
                lbllocation.Text = DB.tblCOUNTRies.Single(p => p.TenentID == TID && p.COUNTRYID == CID).NATIONALITY1;
            }
            else
            {
                lbllocation.Text = "Not Found";
            }
            drpInvoiceID.DataSource = DB.ICTR_HD.Where(p => p.TenentID == TID && p.InvoiceNO !="0" && p.InvoiceNO !="");
            drpInvoiceID.DataTextField = "InvoiceNO";
            drpInvoiceID.DataValueField = "MYTRANSID";
            drpInvoiceID.DataBind();
            drpInvoiceID.Items.Insert(0, new ListItem("-- Select --", "0"));
            
      
        }

    }
}