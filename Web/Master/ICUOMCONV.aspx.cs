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
    public partial class ICUOMCONV : System.Web.UI.Page
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
                //  pnlSuccessMsg.Visible = false;
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
            List<Database.ICUOMCONV> List = DB.ICUOMCONVs.ToList();
            Listview1.DataSource = List;
            Listview1.DataBind();
            //int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
            //int Totalrec = List.Count();
            //((AcmMaster)Page.Master).Loadlist(Showdata, take, Skip, ChoiceID, lblShowinfEntry, btnPrevious1, btnNext1, Listview1, ListView2, Totalrec, List);
        }
        #endregion

        public void GetShow()
        {

            lblFUOM1s.Attributes["class"] = lblTUOM1s.Attributes["class"] = lblCONVERSION1s.Attributes["class"] = lblREMARKS1s.Attributes["class"] = "control-label col-md-4  getshow";
            lblFUOM2h.Attributes["class"] = lblTUOM2h.Attributes["class"] = lblCONVERSION2h.Attributes["class"] = lblREMARKS2h.Attributes["class"] = "control-label col-md-4  gethide";
            b.Attributes.Remove("dir");
            b.Attributes.Add("dir", "ltr");

        }

        public void GetHide()
        {
            lblFUOM1s.Attributes["class"] = lblTUOM1s.Attributes["class"] = lblCONVERSION1s.Attributes["class"] = lblREMARKS1s.Attributes["class"] = "control-label col-md-4  gethide";
            lblFUOM2h.Attributes["class"] = lblTUOM2h.Attributes["class"] = lblCONVERSION2h.Attributes["class"] = lblREMARKS2h.Attributes["class"] = "control-label col-md-4  getshow";
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
            drpFUOM.SelectedIndex = 0;
            txtTUOM.Text = "";
            txtCONVERSION.Text = "";
            txtREMARKS.Text = "";
            //txtCRUP_ID.Text = "";
            //txtUSERID.Text = "";
            //txtENTRYDATE.Text = "";
            //txtENTRYTIME.Text = "";
            //txtUPDTTIME.Text = "";

        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                //try
                //{
                    int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
                    if (btnAdd.Text == "Add New")
                    {

                        Write();
                        Clear();
                        btnAdd.Text = "Add";
                        btnAdd.ValidationGroup = "submit";
                    }
                    else if (btnAdd.Text == "Add")
                    {
                        Database.ICUOM objUOM = new Database.ICUOM();
                        objUOM.TenentID = TID;
                        objUOM.UOM = DB.ICUOMs.Where(p => p.TenentID == TID).Count() > 0 ? Convert.ToInt32(DB.ICUOMs.Where(p => p.TenentID == TID).Max(p=>p.UOM)+1) : 1;
                        objUOM.UOMNAMESHORT = txtTUOM.Text;
                        objUOM.UOMNAME = txtTUOM.Text;
                        objUOM.UOMNAME1 = txtTUOM.Text;
                        objUOM.UOMNAME2 = txtTUOM.Text;
                        objUOM.UOMNAME3 = txtTUOM.Text;
                        objUOM.REMARKS = txtTUOM.Text;
                        objUOM.Active = "Y";
                        DB.ICUOMs.AddObject(objUOM);
                        DB.SaveChanges();

                        Database.ICUOMCONV objICUOMCONV = new Database.ICUOMCONV();
                        //Server Content Send data Yogesh   
                        objICUOMCONV.TenentID = TID;
                        int TUOM = Convert.ToInt32(objUOM.UOM);
                        objICUOMCONV.FUOM = Convert.ToInt32(drpFUOM.SelectedValue);
                        objICUOMCONV.TUOM = TUOM;
                        objICUOMCONV.CONVERSION = Convert.ToDouble(txtCONVERSION.Text);
                        objICUOMCONV.REMARKS = txtREMARKS.Text;

                       // objICUOMCONV.CRUP_ID = txtCRUP_ID.Text;
                      //  objICUOMCONV.USERID = txtUSERID.Text;
                        //objICUOMCONV.ENTRYDATE = Convert.ToDateTime(txtENTRYDATE.Text);
                        //objICUOMCONV.ENTRYTIME = Convert.ToDateTime(txtENTRYTIME.Text);
                        //objICUOMCONV.UPDTTIME = Convert.ToDateTime(txtUPDTTIME.Text);


                        DB.ICUOMCONVs.AddObject(objICUOMCONV);
                        DB.SaveChanges();
                        Clear();
                        btnAdd.Text = "Add New";
                        btnAdd.ValidationGroup = "ss";
                        lblMsg.Text = "  Data Save Successfully";
                        pnlSuccessMsg.Visible = true;
                        BindData();
                      //  navigation.Visible = true;
                        Readonly();
                        //FirstData();
                    }
                    else if (btnAdd.Text == "Update")
                    {

                        if (ViewState["Edit"] != null)
                        {
                            string[] ID = ViewState["Edit"].ToString().Split('-');
                            int fuom = Convert.ToInt32(ID[0]);
                            int tuom = Convert.ToInt32(ID[1]);
                            Database.ICUOMCONV objICUOMCONV = DB.ICUOMCONVs.Single(p => p.TenentID == TID && p.FUOM == fuom && p.TUOM == tuom);                                                        
                            objICUOMCONV.CONVERSION = Convert.ToDouble(txtCONVERSION.Text);
                            objICUOMCONV.REMARKS = txtREMARKS.Text;


                            Database.ICUOM objUOM = DB.ICUOMs.Single(p => p.TenentID == TID && p.UOM == tuom);
                            objUOM.UOMNAMESHORT = txtREMARKS.Text;
                            objUOM.UOMNAME1 = txtREMARKS.Text;
                            objUOM.UOMNAME2 = txtREMARKS.Text;
                            objUOM.UOMNAME3 = txtREMARKS.Text;
                            DB.SaveChanges();
                            //objICUOMCONV.CRUP_ID = txtCRUP_ID.Text;
                            //objICUOMCONV.USERID = txtUSERID.Text;
                            //objICUOMCONV.ENTRYDATE = Convert.ToDateTime(txtENTRYDATE.Text);
                            //objICUOMCONV.ENTRYTIME = Convert.ToDateTime(txtENTRYTIME.Text);
                            //objICUOMCONV.UPDTTIME = Convert.ToDateTime(txtUPDTTIME.Text);

                            ViewState["Edit"] = null;
                            btnAdd.Text = "Add New";
                            btnAdd.ValidationGroup = "ss";
                            DB.SaveChanges();
                            Clear();
                            lblMsg.Text = "  Data Edit Successfully";
                            pnlSuccessMsg.Visible = true;
                            BindData();
                           // navigation.Visible = true;
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
            Response.Redirect("ICUOMCONV.aspx");
        }
        public void FillContractorID()
        {
            //drpUPDTTIME.Items.Insert(0, new ListItem("-- Select --", "0"));drpUPDTTIME.DataSource = DB.0;drpUPDTTIME.DataTextField = "0";drpUPDTTIME.DataValueField = "0";drpUPDTTIME.DataBind();
            int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            drpFUOM.DataSource = DB.ICUOMs.Where(p=>p.TenentID == TID);
            drpFUOM.DataTextField = "UOMNAME1";
            drpFUOM.DataValueField = "UOM";
            drpFUOM.DataBind();
            drpFUOM.Items.Insert(0, new ListItem("-- Select --", "0"));

            //drpTUOM.DataSource = DB.ICUOMs;
            //drpTUOM.DataTextField = "UOMNAME1";
            //drpTUOM.DataValueField = "UOM";
            //drpTUOM.DataBind();
            //drpTUOM.Items.Insert(0, new ListItem("-- Select --", "0"));
        }
        public string GetFUOM(int ID)
        {
            int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            if(DB.ICUOMs.Where(p => p.UOM == ID && p.TenentID == TID).Count() > 0)
            {
                string FUOM = DB.ICUOMs.Single(p => p.UOM == ID && p.TenentID == TID).UOMNAME1.ToString();
                return FUOM;
            }
            else
            {
                return "Not Found";
            }
        }
        public string GetTUOM(int ID)
        {
            int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            if(DB.ICUOMs.Where(p => p.UOM == ID && p.TenentID == TID).Count() > 0)
            {
                string TUOM = DB.ICUOMs.Single(p => p.UOM == ID && p.TenentID == TID).UOMNAME1.ToString();
                return TUOM;
            }
            else
            {
                return "Not Found";
            }
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
            txtTUOM.Text = Listview1.SelectedDataKey[0].ToString();
            txtCONVERSION.Text = Listview1.SelectedDataKey[0].ToString();
            txtREMARKS.Text = Listview1.SelectedDataKey[0].ToString();
            //txtCRUP_ID.Text = Listview1.SelectedDataKey[0].ToString();
            //txtUSERID.Text = Listview1.SelectedDataKey[0].ToString();
            //txtENTRYDATE.Text = Listview1.SelectedDataKey[0].ToString();
            //txtENTRYTIME.Text = Listview1.SelectedDataKey[0].ToString();
            //txtUPDTTIME.Text = Listview1.SelectedDataKey[0].ToString();

        }
        public void NextData()
        {

            if (Listview1.SelectedIndex != Listview1.Items.Count - 1)
            {
                Listview1.SelectedIndex = Listview1.SelectedIndex + 1;
                txtTUOM.Text = Listview1.SelectedDataKey[0].ToString();
                txtCONVERSION.Text = Listview1.SelectedDataKey[0].ToString();
                txtREMARKS.Text = Listview1.SelectedDataKey[0].ToString();
                //txtCRUP_ID.Text = Listview1.SelectedDataKey[0].ToString();
                //txtUSERID.Text = Listview1.SelectedDataKey[0].ToString();
                //txtENTRYDATE.Text = Listview1.SelectedDataKey[0].ToString();
                //txtENTRYTIME.Text = Listview1.SelectedDataKey[0].ToString();
                //txtUPDTTIME.Text = Listview1.SelectedDataKey[0].ToString();

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
                txtTUOM.Text = Listview1.SelectedDataKey[0].ToString();
                txtCONVERSION.Text = Listview1.SelectedDataKey[0].ToString();
                txtREMARKS.Text = Listview1.SelectedDataKey[0].ToString();
                //txtCRUP_ID.Text = Listview1.SelectedDataKey[0].ToString();
                //txtUSERID.Text = Listview1.SelectedDataKey[0].ToString();
                //txtENTRYDATE.Text = Listview1.SelectedDataKey[0].ToString();
                //txtENTRYTIME.Text = Listview1.SelectedDataKey[0].ToString();
                //txtUPDTTIME.Text = Listview1.SelectedDataKey[0].ToString();

            }
        }
        public void LastData()
        {
            Listview1.SelectedIndex = Listview1.Items.Count - 1;
            txtTUOM.Text = Listview1.SelectedDataKey[0].ToString();
            txtCONVERSION.Text = Listview1.SelectedDataKey[0].ToString();
            txtREMARKS.Text = Listview1.SelectedDataKey[0].ToString();
            //txtCRUP_ID.Text = Listview1.SelectedDataKey[0].ToString();
            //txtUSERID.Text = Listview1.SelectedDataKey[0].ToString();
            //txtENTRYDATE.Text = Listview1.SelectedDataKey[0].ToString();
            //txtENTRYTIME.Text = Listview1.SelectedDataKey[0].ToString();
            //txtUPDTTIME.Text = Listview1.SelectedDataKey[0].ToString();

        }


        protected void btnEditLable_Click(object sender, EventArgs e)
        {
            if (Session["LANGUAGE"].ToString() == "ar-KW")
            {
                if (btnEditLable.Text == "Update Label")
                {

                    //2false
                    lblFUOM2h.Visible = lblTUOM2h.Visible = lblCONVERSION2h.Visible = lblREMARKS2h.Visible = false;
                    //2true
                    txtFUOM2h.Visible = txtTUOM2h.Visible = txtCONVERSION2h.Visible = txtREMARKS2h.Visible = true;

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
                    lblFUOM2h.Visible = lblTUOM2h.Visible = lblCONVERSION2h.Visible = lblREMARKS2h.Visible = true;
                    //2false
                    txtFUOM2h.Visible = txtTUOM2h.Visible = txtCONVERSION2h.Visible = txtREMARKS2h.Visible = false;

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
                    lblFUOM1s.Visible = lblTUOM1s.Visible = lblCONVERSION1s.Visible = lblREMARKS1s.Visible = false;
                    //1true
                    txtFUOM1s.Visible = txtTUOM1s.Visible = txtCONVERSION1s.Visible = txtREMARKS1s.Visible = true;
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
                    lblFUOM1s.Visible = lblTUOM1s.Visible = lblCONVERSION1s.Visible = lblREMARKS1s.Visible = true;
                    //1false
                    txtFUOM1s.Visible = txtTUOM1s.Visible = txtCONVERSION1s.Visible = txtREMARKS1s.Visible = false;
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

            List<Database.TBLLabelDTL> List = ((AcmMaster)this.Master).Bindxml("ICUOMCONV").Where(p => p.LabelMstID == PID && p.LANGDISP == lang).ToList();
            foreach (Database.TBLLabelDTL item in List)
            {
                if (lblFUOM1s.ID == item.LabelID)
                    txtFUOM1s.Text = lblFUOM1s.Text = lblhFUOM.Text = item.LabelName;
                else if (lblTUOM1s.ID == item.LabelID)
                    txtTUOM1s.Text = lblTUOM1s.Text = lblhTUOM.Text = item.LabelName;
                else if (lblCONVERSION1s.ID == item.LabelID)
                    txtCONVERSION1s.Text = lblCONVERSION1s.Text = lblhCONVERSION.Text = item.LabelName;
                else if (lblREMARKS1s.ID == item.LabelID)
                    txtREMARKS1s.Text = lblREMARKS1s.Text = lblhREMARKS.Text = item.LabelName;
                

                else if (lblFUOM2h.ID == item.LabelID)
                    txtFUOM2h.Text = lblFUOM2h.Text = lblhFUOM.Text = item.LabelName;
                else if (lblTUOM2h.ID == item.LabelID)
                    txtTUOM2h.Text = lblTUOM2h.Text = lblhTUOM.Text = item.LabelName;
                else if (lblCONVERSION2h.ID == item.LabelID)
                    txtCONVERSION2h.Text = lblCONVERSION2h.Text = lblhCONVERSION.Text = item.LabelName;
                else if (lblREMARKS2h.ID == item.LabelID)
                    txtREMARKS2h.Text = lblREMARKS2h.Text = lblhREMARKS.Text = item.LabelName;

                else
                    txtHeader.Text = lblHeader.Text = Label5.Text = item.LabelName;
            }

        }
        public void SaveLabel(string lang)
        {
            string PID = ((AcmMaster)this.Master).getOwnPage();
            //List<Database.TBLLabelDTL> List = DB.TBLLabelDTLs.Where(p => p.LabelMstID == PID  && p.LANGDISP == lang).ToList();
            List<Database.TBLLabelDTL> List = ((AcmMaster)this.Master).Bindxml("ICUOMCONV").Where(p => p.LabelMstID == PID && p.LANGDISP == lang).ToList();
            DataSet ds = new DataSet();
            ds.ReadXml(Server.MapPath("\\Master\\xml\\ICUOMCONV.xml"));
            foreach (Database.TBLLabelDTL item in List)
            {

                var obj = ((AcmMaster)this.Master).Bindxml("ICUOMCONV").Single(p => p.LabelID == item.LabelID && p.LabelMstID == PID && p.LANGDISP == lang);
                int i = obj.ID - 1;

                if (lblFUOM1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtFUOM1s.Text;
                else if (lblTUOM1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtTUOM1s.Text;
                else if (lblCONVERSION1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtCONVERSION1s.Text;
                else if (lblREMARKS1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtREMARKS1s.Text;
                

                else if (lblFUOM2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtFUOM2h.Text;
                else if (lblTUOM2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtTUOM2h.Text;
                else if (lblCONVERSION2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtCONVERSION2h.Text;
                else if (lblREMARKS2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtREMARKS2h.Text;

                else
                    ds.Tables[0].Rows[i]["LabelName"] = txtHeader.Text;
            }
            ds.WriteXml(Server.MapPath("\\Master\\xml\\ICUOMCONV.xml"));

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
            drpFUOM.Enabled = true;
            txtTUOM.Enabled = true;
            txtCONVERSION.Enabled = true;
            txtREMARKS.Enabled = true;
            //txtCRUP_ID.Enabled = true;
            //txtUSERID.Enabled = true;
            //txtENTRYDATE.Enabled = true;
            //txtENTRYTIME.Enabled = true;
            //txtUPDTTIME.Enabled = true;

        }
        public void Readonly()
        {
           // navigation.Visible = true;
            drpFUOM.Enabled = false;
            txtTUOM.Enabled = false;
            txtCONVERSION.Enabled = false;
            txtREMARKS.Enabled = false;
            //txtCRUP_ID.Enabled = false;
            //txtUSERID.Enabled = false;
            //txtENTRYDATE.Enabled = false;
            //txtENTRYTIME.Enabled = false;
            //txtUPDTTIME.Enabled = false;


        }

        #region Listview
        //protected void btnNext1_Click(object sender, EventArgs e)
        //{

        //    int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
        //    int Totalrec = DB.ICUOMCONVs.Count();
        //    if (ViewState["Take"] == null && ViewState["Skip"] == null)
        //    {
        //        ViewState["Take"] = Showdata;
        //        ViewState["Skip"] = 0;
        //    }
        //    take = Convert.ToInt32(ViewState["Take"]);
        //    take = take + Showdata;
        //    Skip = take - Showdata;

        //    ((AcmMaster)Page.Master).BindList(Listview1, (DB.ICUOMCONVs.Take(take).Skip(Skip)).ToList());
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
        //        int Totalrec = DB.ICUOMCONVs.Count();
        //        Skip = Convert.ToInt32(ViewState["Skip"]);
        //        take = Skip;
        //        Skip = take - Showdata;
        //        ((AcmMaster)Page.Master).BindList(Listview1, (DB.ICUOMCONVs.Take(take).Skip(Skip)).ToList());
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
        //        int Totalrec = DB.ICUOMCONVs.Count();
        //        take = Showdata;
        //        Skip = 0;
        //        ((AcmMaster)Page.Master).BindList(Listview1, (DB.ICUOMCONVs.Take(take).Skip(Skip)).ToList());
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
        //    int Totalrec = DB.ICUOMCONVs.Count();
        //    take = Totalrec;
        //    Skip = Totalrec - Showdata;
        //    ((AcmMaster)Page.Master).BindList(Listview1, (DB.ICUOMCONVs.Take(take).Skip(Skip)).ToList());
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
                int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
                try
                {
                    if (e.CommandName == "btnDelete")
                    {
                        //int ID = Convert.ToInt32(e.CommandArgument);
                        string[] ID = e.CommandArgument.ToString().Split('-');
                        int fuom = Convert.ToInt32(ID[0]);
                        int tuom = Convert.ToInt32(ID[1]);

                        Database.ICUOMCONV objSOJobDesc = DB.ICUOMCONVs.Single(p=>p.TenentID == TID && p.FUOM == fuom && p.TUOM == tuom);
                        int TUOMCON = Convert.ToInt32(objSOJobDesc.TUOM);
                        Database.ICUOM ObjUOMDelete = DB.ICUOMs.Single(p => p.TenentID == TID && p.UOM == TUOMCON);
                        DB.ICUOMs.DeleteObject(ObjUOMDelete);
                        DB.ICUOMCONVs.DeleteObject(objSOJobDesc);
                        
                        DB.SaveChanges();
                        BindData();                       
                    }

                    if (e.CommandName == "btnEdit")
                    {
                        //int ID = Convert.ToInt32(e.CommandArgument);
                        string[] ID = e.CommandArgument.ToString().Split('-');
                        int fuom = Convert.ToInt32(ID[0]);
                        int tuom = Convert.ToInt32(ID[1]);

                        Database.ICUOMCONV objICUOMCONV = DB.ICUOMCONVs.Single(p => p.TenentID == TID && p.FUOM == fuom && p.TUOM == tuom);
                        int TUOMCON = Convert.ToInt32(objICUOMCONV.TUOM);
                        string TUOMCONNAME = DB.ICUOMs.Single(p => p.TenentID == TID && p.UOM == TUOMCON).UOMNAME1;
                        drpFUOM.SelectedValue = objICUOMCONV.FUOM.ToString();
                        txtTUOM.Text = TUOMCONNAME.ToString();
                        txtCONVERSION.Text = objICUOMCONV.CONVERSION.ToString();
                        txtREMARKS.Text = objICUOMCONV.REMARKS.ToString();
                        //txtCRUP_ID.Text = objICUOMCONV.CRUP_ID.ToString();
                        //txtUSERID.Text = objICUOMCONV.USERID.ToString();
                        //txtENTRYDATE.Text = objICUOMCONV.ENTRYDATE.ToString();
                        //txtENTRYTIME.Text = objICUOMCONV.ENTRYTIME.ToString();
                        //txtUPDTTIME.Text = objICUOMCONV.UPDTTIME.ToString();

                        btnAdd.Text = "Update";
                        ViewState["Edit"] = fuom + "-" + tuom;
                        Write();
                        drpFUOM.Enabled = false;
                        txtTUOM.Enabled = false;
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
        //    int Totalrec = DB.ICUOMCONVs.Count();
        //    if (e.CommandName == "LinkPageavigation")
        //    {
        //        int ID = Convert.ToInt32(e.CommandArgument);
        //        ViewState["Take"] = ID * Showdata;
        //        ViewState["Skip"] = (ID * Showdata) - Showdata;
        //        int Tvalue = Convert.ToInt32(ViewState["Take"]);
        //        int Svalue = Convert.ToInt32(ViewState["Skip"]);
        //        ((AcmMaster)Page.Master).BindList(Listview1, (DB.ICUOMCONVs.Take(Tvalue).Skip(Svalue)).ToList());
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

    }
}