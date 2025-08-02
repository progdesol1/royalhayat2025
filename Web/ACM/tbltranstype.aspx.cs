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

namespace Web.ACM
{
    public partial class tbltranstype : System.Web.UI.Page
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
               // FirstData();

            }
        }
        #region Step2
        public void BindData()
        {
            int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            var List = DB.tbltranstypes.Where(p=>p.TenentID==TID).OrderBy(m => m.transid).ToList();
            int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
            int Totalrec = List.Count();
            ((ACMMaster)Page.Master).Loadlist(Showdata, take, Skip, ChoiceID, lblShowinfEntry, btnPrevious1, btnNext1, Listview1, ListView2, Totalrec, List);
        }
        #endregion

        #region PAge Genarator
        

        public void GetShow()
        {

             lblMYSYSNAME1s.Attributes["class"] = lblinoutSwitch1s.Attributes["class"] = lbltranstype11s.Attributes["class"] = lbltranstype21s.Attributes["class"] = lbltranstype31s.Attributes["class"] = lblserialno1s.Attributes["class"] = lblyears1s.Attributes["class"] = lblActive1s.Attributes["class"] = lbltranstype1s.Attributes["class"] = "control-label col-md-4  getshow";
             lblMYSYSNAME2h.Attributes["class"] = lblinoutSwitch2h.Attributes["class"] = lbltranstype12h.Attributes["class"] = lbltranstype22h.Attributes["class"] = lbltranstype32h.Attributes["class"] = lblserialno2h.Attributes["class"] = lblyears2h.Attributes["class"] = lblActive2h.Attributes["class"] =  lbltranstype2h.Attributes["class"] = "control-label col-md-4  gethide";
            b.Attributes.Remove("dir");
            b.Attributes.Add("dir", "ltr");

        }

        public void GetHide()
        {
            lblMYSYSNAME1s.Attributes["class"] = lblinoutSwitch1s.Attributes["class"] = lbltranstype11s.Attributes["class"] = lbltranstype21s.Attributes["class"] = lbltranstype31s.Attributes["class"] = lblserialno1s.Attributes["class"] = lblyears1s.Attributes["class"] = lblActive1s.Attributes["class"] =  lbltranstype1s.Attributes["class"] = "control-label col-md-4  gethide";
             lblMYSYSNAME2h.Attributes["class"] = lblinoutSwitch2h.Attributes["class"] = lbltranstype12h.Attributes["class"] = lbltranstype22h.Attributes["class"] = lbltranstype32h.Attributes["class"] = lblserialno2h.Attributes["class"] = lblyears2h.Attributes["class"] = lblActive2h.Attributes["class"] =  lbltranstype2h.Attributes["class"] = "control-label col-md-4  getshow";
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
        public void Clear()
        {
            //drptransid.SelectedIndex = 0;
            drpMYSYSNAME.SelectedIndex = 0;
            txtinoutSwitch.Text = "";
            txttranstype1.Text = "";
            txttranstype2.Text = "";
            txttranstype3.Text = "";
            txtserialno.Text = "";
            txtyears.Text = "";
            cbActive.Checked = false ;
           // txtCRUP_ID.Text = "";
            txttranstype.Text = "";

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
                        Database.tbltranstype objtbltranstype = new Database.tbltranstype();
                        //Server Content Send data Yogesh
                        objtbltranstype.transid = DB.tbltranstypes.Count() > 0 ? Convert.ToInt32(DB.tbltranstypes.Max(p => p.transid) + 1) : 1;
                        objtbltranstype.MYSYSNAME = drpMYSYSNAME.SelectedValue;
                        objtbltranstype.inoutSwitch = txtinoutSwitch.Text;
                        objtbltranstype.transtype1 = txttranstype1.Text;
                        objtbltranstype.transtype2 = txttranstype2.Text;
                        objtbltranstype.transtype3 = txttranstype3.Text;
                        objtbltranstype.serialno = txtserialno.Text;
                        objtbltranstype.years = txtyears.Text;
                        objtbltranstype.Active = cbActive.Checked==true ?"Y":"N";
                      //  objtbltranstype.CRUP_ID = txtCRUP_ID.Text;
                        objtbltranstype.transtype = txttranstype.Text;


                        DB.tbltranstypes.AddObject(objtbltranstype);
                        DB.SaveChanges();
                       // Clear();
                        lblMsg.Text = "  Data Save Successfully";
                        pnlSuccessMsg.Visible = true;
                      //  BindData();
                      //  navigation.Visible = true;
                        Readonly();
                      //  FirstData();
                    }
                    else if (btnAdd.Text == "Update")
                    {

                        if (ViewState["Edit"] != null)
                        {
                            int ID = Convert.ToInt32(ViewState["Edit"]);
                            Database.tbltranstype objtbltranstype = DB.tbltranstypes.Single(p => p.transid == ID);
                           
                            objtbltranstype.MYSYSNAME = drpMYSYSNAME.SelectedValue;
                            objtbltranstype.inoutSwitch = txtinoutSwitch.Text;
                            objtbltranstype.transtype1 = txttranstype1.Text;
                            objtbltranstype.transtype2 = txttranstype2.Text;
                            objtbltranstype.transtype3 = txttranstype3.Text;
                            objtbltranstype.serialno = txtserialno.Text;
                            objtbltranstype.years = txtyears.Text;
                            objtbltranstype.Active = cbActive.Checked == true ? "Y" : "N";
                            //objtbltranstype.CRUP_ID = txtCRUP_ID.Text;
                            objtbltranstype.transtype = txttranstype.Text;

                            ViewState["Edit"] = null;
                            btnAdd.Text = "AddNew";
                            DB.SaveChanges();
                           // Clear();
                            lblMsg.Text = "  Data Edit Successfully";
                            pnlSuccessMsg.Visible = true;
                          //  BindData();
                           // navigation.Visible = true;
                            Readonly();
                           // FirstData();
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
            Classes.EcommAdminClass.getdropdown(drpMYSYSNAME, 0, "", "", "", "TBLSYSTEMS");
            //select * from TBLSYSTEMS where ACTIVE = 1

            //drpMYSYSNAME.DataSource = DB.TBLSYSTEMS.Where(P => P.ACTIVE == "1");
            //drpMYSYSNAME.DataTextField = "SYSDESC1";
            //drpMYSYSNAME.DataValueField = "MYSYSNAME";
            //drpMYSYSNAME.DataBind();
            //drpMYSYSNAME.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select System--", "0"));
        }

        #region PAge Genarator navigation
        
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
            //drptransid.SelectedValue = Listview1.SelectedDataKey[0].ToString();
            //txtMYSYSNAME.Text = Listview1.SelectedDataKey[0].ToString();
            txtinoutSwitch.Text = Listview1.SelectedDataKey[0].ToString();
            txttranstype1.Text = Listview1.SelectedDataKey[0].ToString();
            txttranstype2.Text = Listview1.SelectedDataKey[0].ToString();
            txttranstype3.Text = Listview1.SelectedDataKey[0].ToString();
            txtserialno.Text = Listview1.SelectedDataKey[0].ToString();
            txtyears.Text = Listview1.SelectedDataKey[0].ToString();
           // txtActive.Text = Listview1.SelectedDataKey[0].ToString();
          //  txtCRUP_ID.Text = Listview1.SelectedDataKey[0].ToString();
            txttranstype.Text = Listview1.SelectedDataKey[0].ToString();

        }
        public void NextData()
        {

            if (Listview1.SelectedIndex != Listview1.Items.Count - 1)
            {
                Listview1.SelectedIndex = Listview1.SelectedIndex + 1;
                //drptransid.SelectedValue = Listview1.SelectedDataKey[0].ToString();
                //txtMYSYSNAME.Text = Listview1.SelectedDataKey[0].ToString();
                txtinoutSwitch.Text = Listview1.SelectedDataKey[0].ToString();
                txttranstype1.Text = Listview1.SelectedDataKey[0].ToString();
                txttranstype2.Text = Listview1.SelectedDataKey[0].ToString();
                txttranstype3.Text = Listview1.SelectedDataKey[0].ToString();
                txtserialno.Text = Listview1.SelectedDataKey[0].ToString();
                txtyears.Text = Listview1.SelectedDataKey[0].ToString();
               // txtActive.Text = Listview1.SelectedDataKey[0].ToString();
               // txtCRUP_ID.Text = Listview1.SelectedDataKey[0].ToString();
                txttranstype.Text = Listview1.SelectedDataKey[0].ToString();

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
               // drptransid.SelectedValue = Listview1.SelectedDataKey[0].ToString();
               // txtMYSYSNAME.Text = Listview1.SelectedDataKey[0].ToString();
                txtinoutSwitch.Text = Listview1.SelectedDataKey[0].ToString();
                txttranstype1.Text = Listview1.SelectedDataKey[0].ToString();
                txttranstype2.Text = Listview1.SelectedDataKey[0].ToString();
                txttranstype3.Text = Listview1.SelectedDataKey[0].ToString();
                txtserialno.Text = Listview1.SelectedDataKey[0].ToString();
                txtyears.Text = Listview1.SelectedDataKey[0].ToString();
               // txtActive.Text = Listview1.SelectedDataKey[0].ToString();
              //  txtCRUP_ID.Text = Listview1.SelectedDataKey[0].ToString();
                txttranstype.Text = Listview1.SelectedDataKey[0].ToString();

            }
        }
        public void LastData()
        {
            Listview1.SelectedIndex = Listview1.Items.Count - 1;
          //  drptransid.SelectedValue = Listview1.SelectedDataKey[0].ToString();
          ///  txtMYSYSNAME.Text = Listview1.SelectedDataKey[0].ToString();
            txtinoutSwitch.Text = Listview1.SelectedDataKey[0].ToString();
            txttranstype1.Text = Listview1.SelectedDataKey[0].ToString();
            txttranstype2.Text = Listview1.SelectedDataKey[0].ToString();
            txttranstype3.Text = Listview1.SelectedDataKey[0].ToString();
            txtserialno.Text = Listview1.SelectedDataKey[0].ToString();
            txtyears.Text = Listview1.SelectedDataKey[0].ToString();
          //  txtActive.Text = Listview1.SelectedDataKey[0].ToString();
           // txtCRUP_ID.Text = Listview1.SelectedDataKey[0].ToString();
            txttranstype.Text = Listview1.SelectedDataKey[0].ToString();

        }
        #endregion

        #region PAge Genarator language
        protected void btnEditLable_Click(object sender, EventArgs e)
        {
            if (Session["LANGUAGE"].ToString() == "ar-KW")
            {
                if (btnEditLable.Text == "Update Label")
                {

                    //2false
                    lblMYSYSNAME2h.Visible = lblinoutSwitch2h.Visible = lbltranstype12h.Visible = lbltranstype22h.Visible = lbltranstype32h.Visible = lblserialno2h.Visible = lblyears2h.Visible = lblActive2h.Visible =  lbltranstype2h.Visible = false;
                    //2true
                     txtMYSYSNAME2h.Visible = txtinoutSwitch2h.Visible = txttranstype12h.Visible = txttranstype22h.Visible = txttranstype32h.Visible = txtserialno2h.Visible = txtyears2h.Visible = txtActive2h.Visible =  txttranstype2h.Visible = true;

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
                     lblMYSYSNAME2h.Visible = lblinoutSwitch2h.Visible = lbltranstype12h.Visible = lbltranstype22h.Visible = lbltranstype32h.Visible = lblserialno2h.Visible = lblyears2h.Visible = lblActive2h.Visible = lbltranstype2h.Visible = true;
                    //2false
                     txtMYSYSNAME2h.Visible = txtinoutSwitch2h.Visible = txttranstype12h.Visible = txttranstype22h.Visible = txttranstype32h.Visible = txtserialno2h.Visible = txtyears2h.Visible = txtActive2h.Visible =  txttranstype2h.Visible = false;

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
                     lblMYSYSNAME1s.Visible = lblinoutSwitch1s.Visible = lbltranstype11s.Visible = lbltranstype21s.Visible = lbltranstype31s.Visible = lblserialno1s.Visible = lblyears1s.Visible = lblActive1s.Visible = lbltranstype1s.Visible = false;
                    //1true
                    txtMYSYSNAME1s.Visible = txtinoutSwitch1s.Visible = txttranstype11s.Visible = txttranstype21s.Visible = txttranstype31s.Visible = txtserialno1s.Visible = txtyears1s.Visible = txtActive1s.Visible =  txttranstype1s.Visible = true;
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
                   lblMYSYSNAME1s.Visible = lblinoutSwitch1s.Visible = lbltranstype11s.Visible = lbltranstype21s.Visible = lbltranstype31s.Visible = lblserialno1s.Visible = lblyears1s.Visible = lblActive1s.Visible =  lbltranstype1s.Visible = true;
                    //1false
                    txtMYSYSNAME1s.Visible = txtinoutSwitch1s.Visible = txttranstype11s.Visible = txttranstype21s.Visible = txttranstype31s.Visible = txtserialno1s.Visible = txtyears1s.Visible = txtActive1s.Visible =  txttranstype1s.Visible = false;
                    //header
                    lblHeader.Visible = true;
                    txtHeader.Visible = false;
                }
            }
        }
        public void RecieveLabel(string lang)
        {
            string str = "";
            string PID = ((ACMMaster)this.Master).getOwnPage();

            List<Database.TBLLabelDTL> List = ((ACMMaster)this.Master).Bindxml("tbltranstype").Where(p => p.LabelMstID == PID && p.LANGDISP == lang).ToList();
            foreach (Database.TBLLabelDTL item in List)
            {
               if (lblMYSYSNAME1s.ID == item.LabelID)
                    txtMYSYSNAME1s.Text = lblMYSYSNAME1s.Text = lblhMYSYSNAME.Text = item.LabelName;
                else if (lblinoutSwitch1s.ID == item.LabelID)
                    txtinoutSwitch1s.Text = lblinoutSwitch1s.Text =  item.LabelName;
                else if (lbltranstype11s.ID == item.LabelID)
                    txttranstype11s.Text = lbltranstype11s.Text = lblhtranstype1.Text = item.LabelName;
                else if (lbltranstype21s.ID == item.LabelID)
                    txttranstype21s.Text = lbltranstype21s.Text = item.LabelName;
                else if (lbltranstype31s.ID == item.LabelID)
                    txttranstype31s.Text = lbltranstype31s.Text =  item.LabelName;
                else if (lblserialno1s.ID == item.LabelID)
                    txtserialno1s.Text = lblserialno1s.Text = lblhserialno.Text = item.LabelName;
                else if (lblyears1s.ID == item.LabelID)
                    txtyears1s.Text = lblyears1s.Text = lblhyears.Text = item.LabelName;
                else if (lblActive1s.ID == item.LabelID)
                    txtActive1s.Text = lblActive1s.Text = lblhActive.Text = item.LabelName;
               
                else if (lbltranstype1s.ID == item.LabelID)
                    txttranstype1s.Text = lbltranstype1s.Text =  item.LabelName;
                else if (lblMYSYSNAME2h.ID == item.LabelID)
                    txtMYSYSNAME2h.Text = lblMYSYSNAME2h.Text = lblhMYSYSNAME.Text = item.LabelName;
                else if (lblinoutSwitch2h.ID == item.LabelID)
                    txtinoutSwitch2h.Text = lblinoutSwitch2h.Text =  item.LabelName;
                else if (lbltranstype12h.ID == item.LabelID)
                    txttranstype12h.Text = lbltranstype12h.Text = lblhtranstype1.Text = item.LabelName;
                else if (lbltranstype22h.ID == item.LabelID)
                    txttranstype22h.Text = lbltranstype22h.Text =  item.LabelName;
                else if (lbltranstype32h.ID == item.LabelID)
                    txttranstype32h.Text = lbltranstype32h.Text =  item.LabelName;
                else if (lblserialno2h.ID == item.LabelID)
                    txtserialno2h.Text = lblserialno2h.Text = lblhserialno.Text = item.LabelName;
                else if (lblyears2h.ID == item.LabelID)
                    txtyears2h.Text = lblyears2h.Text = lblhyears.Text = item.LabelName;
                else if (lblActive2h.ID == item.LabelID)
                    txtActive2h.Text = lblActive2h.Text = lblhActive.Text = item.LabelName;
               
                else if (lbltranstype2h.ID == item.LabelID)
                    txttranstype2h.Text = lbltranstype2h.Text =  item.LabelName;

                else
                    txtHeader.Text = lblHeader.Text = Label5.Text = item.LabelName;
            }

        }
        public void SaveLabel(string lang)
        {
            string PID = ((ACMMaster)this.Master).getOwnPage();
            //List<Database.TBLLabelDTL> List = DB.TBLLabelDTLs.Where(p => p.LabelMstID == PID  && p.LANGDISP == lang).ToList();
            List<Database.TBLLabelDTL> List = ((ACMMaster)this.Master).Bindxml("tbltranstype.xml").Where(p => p.LabelMstID == PID && p.LANGDISP == lang).ToList();
            DataSet ds = new DataSet();
            ds.ReadXml(Server.MapPath("\\xml\\tbltranstype.xml"));
            foreach (Database.TBLLabelDTL item in List)
            {

                var obj = ((ACMMaster)this.Master).Bindxml("tbltranstype").Single(p => p.LabelID == item.LabelID && p.LabelMstID == PID && p.LANGDISP == lang);
                int i = obj.ID - 1;

               if (lblMYSYSNAME1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtMYSYSNAME1s.Text;
                else if (lblinoutSwitch1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtinoutSwitch1s.Text;
                else if (lbltranstype11s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txttranstype11s.Text;
                else if (lbltranstype21s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txttranstype21s.Text;
                else if (lbltranstype31s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txttranstype31s.Text;
                else if (lblserialno1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtserialno1s.Text;
                else if (lblyears1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtyears1s.Text;
                else if (lblActive1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtActive1s.Text;
               
                else if (lbltranstype1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txttranstype1s.Text;
                else if (lblMYSYSNAME2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtMYSYSNAME2h.Text;
                else if (lblinoutSwitch2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtinoutSwitch2h.Text;
                else if (lbltranstype12h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txttranstype12h.Text;
                else if (lbltranstype22h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txttranstype22h.Text;
                else if (lbltranstype32h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txttranstype32h.Text;
                else if (lblserialno2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtserialno2h.Text;
                else if (lblyears2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtyears2h.Text;
                else if (lblActive2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtActive2h.Text;
               
                else if (lbltranstype2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txttranstype2h.Text;

                else
                    ds.Tables[0].Rows[i]["LabelName"] = txtHeader.Text;
            }
            ds.WriteXml(Server.MapPath("\\xml\\tbltranstype.xml"));

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

        #endregion
        public void Write()
        {
            //navigation.Visible = false;
            //drptransid.Enabled = true;
            drpMYSYSNAME.Enabled = true;
            txtinoutSwitch.Enabled = true;
            txttranstype1.Enabled = true;
            txttranstype2.Enabled = true;
            txttranstype3.Enabled = true;
            txtserialno.Enabled = true;
            txtyears.Enabled = true;
           cbActive.Enabled = true;
            //txtCRUP_ID.Enabled = true;
            txttranstype.Enabled = true;

        }
        public void Readonly()
        {
            //navigation.Visible = true;
            //drptransid.Enabled = false;
            drpMYSYSNAME.Enabled = false;
            txtinoutSwitch.Enabled = false;
            txttranstype1.Enabled = false;
            txttranstype2.Enabled = false;
            txttranstype3.Enabled = false;
            txtserialno.Enabled = false;
            txtyears.Enabled = false;
            cbActive.Enabled = false;
            //txtCRUP_ID.Enabled = false;
            txttranstype.Enabled = false;


        }

        #region Listview
        protected void btnNext1_Click(object sender, EventArgs e)
        {
            int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);

            int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
            int Totalrec = DB.tbltranstypes.Count();
            if (ViewState["Take"] == null && ViewState["Skip"] == null)
            {
                ViewState["Take"] = Showdata;
                ViewState["Skip"] = 0;
            }
            take = Convert.ToInt32(ViewState["Take"]);
            take = take + Showdata;
            Skip = take - Showdata;

            ((ACMMaster)Page.Master).BindList(Listview1, (DB.tbltranstypes.Where(p=>p.TenentID==TID).OrderBy(m => m.transid).Take(take).Skip(Skip)).ToList());
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

            ((ACMMaster)Page.Master).GetCurrentNavigation(ChoiceID, Showdata, ListView2, Totalrec);
            lblShowinfEntry.Text = "Showing " + Skip + " to " + take + " of " + Totalrec + " entries";

        }
        protected void btnPrevious1_Click(object sender, EventArgs e)
        {
            int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
            if (ViewState["Take"] != null && ViewState["Skip"] != null)
            {
                int Totalrec = DB.tbltranstypes.Count();
                Skip = Convert.ToInt32(ViewState["Skip"]);
                take = Skip;
                Skip = take - Showdata;
                ((ACMMaster)Page.Master).BindList(Listview1, (DB.tbltranstypes.Where(p=>p.TenentID==TID).OrderBy(m => m.transid).Take(take).Skip(Skip)).ToList());
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
                ((ACMMaster)Page.Master).GetCurrentNavigation(ChoiceID, Showdata, ListView2, Totalrec);
                lblShowinfEntry.Text = "Showing " + Skip + " to " + take + " of " + Totalrec + " entries";
            }
        }
        protected void btnfirst_Click(object sender, EventArgs e)
        {
            int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
            if (ViewState["Take"] != null && ViewState["Skip"] != null)
            {
                int Totalrec = DB.tbltranstypes.Count();
                take = Showdata;
                Skip = 0;
                ((ACMMaster)Page.Master).BindList(Listview1, (DB.tbltranstypes.Where(p=>p.TenentID==TID).OrderBy(m => m.transid).Take(take).Skip(Skip)).ToList());
                ViewState["Take"] = take;
                ViewState["Skip"] = Skip;
                btnPrevious1.Enabled = false;
                ChoiceID = 0;
                ((ACMMaster)Page.Master).GetCurrentNavigation(ChoiceID, Showdata, ListView2, Totalrec);
                if (take == Totalrec && Skip == (Totalrec - Showdata))
                    btnNext1.Enabled = false;
                else
                    btnNext1.Enabled = true;
                lblShowinfEntry.Text = "Showing " + Skip + " to " + take + " of " + Totalrec + " entries";

            }
        }
        protected void btnLast1_Click(object sender, EventArgs e)
        {
            int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
            int Totalrec = DB.tbltranstypes.Count();
            take = Totalrec;
            Skip = Totalrec - Showdata;
            ((ACMMaster)Page.Master).BindList(Listview1, (DB.tbltranstypes.Where(p=>p.TenentID==TID).OrderBy(m => m.transid).Take(take).Skip(Skip)).ToList());
            ViewState["Take"] = take;
            ViewState["Skip"] = Skip;
            btnNext1.Enabled = false;
            btnPrevious1.Enabled = true;
            ChoiceID = take / Showdata;
            ((ACMMaster)Page.Master).GetCurrentNavigationLast(ChoiceID, Showdata, ListView2, Totalrec);
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
            int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                   // int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TENANT_ID);
                    if (e.CommandName == "btnDelete")
                    {
                       
                        int ID = Convert.ToInt32(e.CommandArgument);
                        Database.tbltranstype objSOJobDesc = DB.tbltranstypes.Single(p => p.transid == ID );
                        objSOJobDesc.Active = "N";
                        DB.SaveChanges();
                        BindData();
                        int Tvalue = Convert.ToInt32(ViewState["Take"]);
                        int Svalue = Convert.ToInt32(ViewState["Skip"]);
                        ((ACMMaster)Page.Master).BindList(Listview1, (DB.tbltranstypes.Where(p=>p.TenentID==TID).OrderBy(m => m.transid).Take(Tvalue).Skip(Svalue)).ToList());

                    }

                    if (e.CommandName == "btnEdit")
                    {
                        int ID = Convert.ToInt32(e.CommandArgument);

                        Database.tbltranstype objtbltranstype = DB.tbltranstypes.Single(p => p.transid == ID );
                        //drptransid.SelectedValue = objtbltranstype.transid.ToString();
                        drpMYSYSNAME.SelectedValue = objtbltranstype.MYSYSNAME.ToString();
                        txtinoutSwitch.Text = objtbltranstype.inoutSwitch;
                        txttranstype1.Text = objtbltranstype.transtype1;
                        txttranstype2.Text = objtbltranstype.transtype2;
                        txttranstype3.Text = objtbltranstype.transtype3;
                        txtserialno.Text = objtbltranstype.serialno;
                        txtyears.Text = objtbltranstype.years;
                        cbActive.Checked = objtbltranstype.Active=="Y"?true :false;
                       // txtCRUP_ID.Text = objtbltranstype.CRUP_ID.ToString();
                        txttranstype.Text = objtbltranstype.transtype;

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
            int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
            int Totalrec = DB.tbltranstypes.Count();
            if (e.CommandName == "LinkPageavigation")
            {
                int ID = Convert.ToInt32(e.CommandArgument);
                ViewState["Take"] = ID * Showdata;
                ViewState["Skip"] = (ID * Showdata) - Showdata;
                int Tvalue = Convert.ToInt32(ViewState["Take"]);
                int Svalue = Convert.ToInt32(ViewState["Skip"]);
                ((ACMMaster)Page.Master).BindList(Listview1, (DB.tbltranstypes.Where(p=>p.TenentID==TID).OrderBy(m => m.transid).Take(Tvalue).Skip(Svalue)).ToList());
                ChoiceID = ID;
                ((ACMMaster)Page.Master).GetCurrentNavigation(ChoiceID, Showdata, ListView2, Totalrec);
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