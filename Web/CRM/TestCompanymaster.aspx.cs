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
using Database;
using System.Collections.Generic;
using System.Transactions;
using System.Globalization;
using System.Resources;
using System.Threading;
using System.Linq.Expressions;
using System.Net;
using System.IO;


namespace Web.CRM
{
    public partial class TestCompanymaster : System.Web.UI.Page
    {
        int count = 0;
        int take = 0;
        int Skip = 0;
        public static int ChoiceID = 0;
        Database.CallEntities DB = new Database.CallEntities();
        List<CAT_MST> List = new List<CAT_MST>();
        List<TBLCOMPANYSETUP> ListTBLCOMPANYSETUP = new List<TBLCOMPANYSETUP>();
        List<tblCONTACTBu> ListtblCONTACTBus = new List<tblCONTACTBu>();
        bool flag = false;
        string languageId = "";
        bool FirstFlag, ClickFlag = true;
        int TID, LID, stMYTRANSID, UID, MID, EMPID, Transid, Transsubid, CID = 0;
        string LangID, CURRENCY, USERID, Crypath = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionLoad();
            panelMsg.Visible = false;
            if (!IsPostBack)
            {
                FistTimeLoad();
                ViewState["SaveList1"] = null;
                Session["Pagename"] = "Suppliers";
                //  ModalPopupExtender4.Hide();
                //    ModalPopupExtender4.TargetControlID = "btntest4";
              //  ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Filling the data Please wait  ...');", true);
                FillContractorID();
              //  ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Grid data are being filled.Please wait ...');", true);
                BindData();
              //  GridBind();
              //  binddata1();
                redonlyfalse();
                BindTitleData();
             //   TabName.Value = Request.Form[TabName.UniqueID];
                if (flag == false)
                {
                 //   ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Preparing the screen display, Please wait ..');", true);
                    //Lastdataredmode();
                    Firstdataredmode();
                    flag = true;
                }
                if (Request.QueryString["Sesstion"] != null)
                {
                    if (Session["SerchList"] != null)
                    {
                        var List = ((List<Database.TBLCOMPANYSETUP>)Session["SerchList"]).ToList();
                        Listview1.DataSource = List;
                        Listview1.DataBind();
                        //int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
                        //int Totalrec = List.Count();
                        //((CRMMaster)Page.Master).Loadlist(Showdata, take, Skip, ChoiceID, lblShowinfEntry, btnPrevious1, btnNext1, Listview1, ListView2, Totalrec, List);
                    }
                }
                if (Request.QueryString["COMPID"] != null)
                {
                    int ContactMyID = Convert.ToInt32(Request.QueryString["COMPID"]);
                    BindEditcompniy(ContactMyID);
                    lblAttecmentcount.Text = Countatt(ContactMyID);
                    btnattmentmst.Visible = true;
                    if (Request.QueryString["Mode"] != null)
                    {
                        string Mode = Request.QueryString["Mode"].ToString();
                        if (Mode == "Write")
                        {
                            redonlyture();
                            btnSubmit.Text = "Update";
                            btnSubmit.Visible = true;
                            Button1.Visible = false;
                            lblBusContactDe.Text = "Business Company Details - (Write Mode)";
                            lblBCDeta.Text = "Business Company Details - (Write Mode)";
                            lblwExistance.Text = "Web Existance - (Write Mode)";
                            lblWEmp.Text = "Working Employees - (Write Mode)";
                        }
                        else
                        {

                            redonlyfalse();
                            btnSubmit.Visible = false;
                            Button1.Visible = true;
                            lblAttecmentcount.Text = Countatt(ContactMyID);
                            btnattmentmst.Visible = true;
                            lblBusContactDe.Text = "Business Company Details -  (Read Mode)";
                            lblBCDeta.Text = "Business Company Details -  (Read Mode)";
                            lblwExistance.Text = "Web Existance -  (Read Mode)";
                            lblWEmp.Text = "Working Employees -  (Read Mode)";

                        }
                        var List = DB.TBLCOMPANYSETUPs.Where(p => p.COMPID == ContactMyID && p.TenentID == TID).ToList();
                        ViewState["SaveList"] = List;
                        //int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
                        //int Totalrec = List.Count();
                        //((CRMMaster)Page.Master).Loadlist(Showdata, take, Skip, ChoiceID, lblShowinfEntry, btnPrevious1, btnNext1, Listview1, ListView2, Totalrec, List);
                        Listview1.DataSource = List;
                        Listview1.DataBind();
                    }
                }
                if (Request.QueryString["NewContect"] != null)
                {
                 
                    if (Session["ListTBLCOMPANYSETUP"] != null)
                    {
                        ListTBLCOMPANYSETUP = (List<Database.TBLCOMPANYSETUP>)Session["ListTBLCOMPANYSETUP"];
                        txtCustomerName.Text = ListTBLCOMPANYSETUP[0].COMPNAME1;
                        txtCustomer.Text = ListTBLCOMPANYSETUP[0].COMPNAME2;
                        txtCustomer2.Text = ListTBLCOMPANYSETUP[0].COMPNAME3;
                        tags_2.Text = ListTBLCOMPANYSETUP[0].EMAIL1;
                        if (ListTBLCOMPANYSETUP[0].ITMANAGER != null || ListTBLCOMPANYSETUP[0].ITMANAGER != "0")
                        {
                            drpItManager.SelectedItem.Text = ListTBLCOMPANYSETUP[0].ITMANAGER;
                        }

                        txtAddress.Text = ListTBLCOMPANYSETUP[0].ADDR1;
                        txtAddress2.Text = ListTBLCOMPANYSETUP[0].ADDR2;
                        txtCity.Text = ListTBLCOMPANYSETUP[0].CITY;

                        txtPostalCode.Text = ListTBLCOMPANYSETUP[0].POSTALCODE;
                        txtZipCode.Text = ListTBLCOMPANYSETUP[0].ZIPCODE;
                        drpMyCounLocID.SelectedValue = ListTBLCOMPANYSETUP[0].MYCONLOCID.ToString();
                        if (ListTBLCOMPANYSETUP[0].MYPRODID != null && ListTBLCOMPANYSETUP[0].MYPRODID != 0)
                            drpMyProductId.SelectedValue = ListTBLCOMPANYSETUP[0].MYPRODID.ToString();

                        tags_4.Text = ListTBLCOMPANYSETUP[0].BUSPHONE1;
                        txtMobileNo.Text = ListTBLCOMPANYSETUP[0].MOBPHONE;
                        tags_3.Text = ListTBLCOMPANYSETUP[0].FAX;
                        int PLID = Convert.ToInt32(ListTBLCOMPANYSETUP[0].PRIMLANGUGE);
                        drpPrimaryLang.SelectedValue = DB.tblLanguages.Single(p => p.MYCONLOCID == PLID && p.TenentID == TID).MYCONLOCID.ToString();
                        txtWebsite.Text = ListTBLCOMPANYSETUP[0].WEBPAGE;
                        if (ListTBLCOMPANYSETUP[0].CompanyType != null && Convert.ToInt32(ListTBLCOMPANYSETUP[0].CompanyType) != 0)
                        {
                            drpType.SelectedValue = (ListTBLCOMPANYSETUP[0].CompanyType).ToString();
                        }

                        chbIsMinistry.Checked = (ListTBLCOMPANYSETUP[0].ISMINISTRY == true) ? true : false;
                        chbIssMb.Checked = (ListTBLCOMPANYSETUP[0].ISSMB == true) ? true : false;
                        chbIsCorporate.Checked = (ListTBLCOMPANYSETUP[0].ISCORPORATE == true) ? true : false;
                        chbInHawally.Checked = (ListTBLCOMPANYSETUP[0].INHAWALLY == true) ? true : false;
                        chbSaler.Checked = (ListTBLCOMPANYSETUP[0].SALER == true) ? true : false;
                        chbBuyer.Checked = (ListTBLCOMPANYSETUP[0].BUYER == true) ? true : false;
                        chbSaleDeProd.Checked = (ListTBLCOMPANYSETUP[0].SALEDEPROD == true) ? true : false;
                        chbEmailSub.Checked = (ListTBLCOMPANYSETUP[0].EMAISUB == true) ? true : false;
                        tags_5.Text = ListTBLCOMPANYSETUP[0].PRODUCTDEALIN;
                        txtRemark.Text = ListTBLCOMPANYSETUP[0].REMARKS;
                        tags_1.Text = ListTBLCOMPANYSETUP[0].Keyword;
                        txtcUserName.Text = ListTBLCOMPANYSETUP[0].CUSERID;
                        txtcPassword.Text = ListTBLCOMPANYSETUP[0].CPASSWRD;
                        string COID = ListTBLCOMPANYSETUP[0].COUNTRYID.ToString();
                        //  int COID = Convert.ToInt32(ListTBLCOMPANYSETUP[0].COUNTRYID);
                        drpCountry.SelectedValue = COID;
                        bindSates(COID);
                        if (ListTBLCOMPANYSETUP[0].STATE != null)
                        {
                            drpSates.SelectedValue = ListTBLCOMPANYSETUP[0].STATE;
                        }
                    }
                    redonlyture();
                    Button1.Visible = false;
                    btnSubmit.Visible = true;

                }
            }


            //for localization
            //Session["Pagename"] = "Suppliers";
            //if (!string.IsNullOrEmpty(Session["Language"] as string))
            //{
            //    if (Session["Language"].ToString().StartsWith("ar-KW") == true)
            //    {
            //        b.Attributes.Remove("dir");
            //        b.Attributes.Add("dir", "rtl");
            //        GetShow();
            //    }
            //    else
            //    {
            //        b.Attributes.Remove("dir");
            //        b.Attributes.Add("dir", "ltr");

            //        GetHide();
            //    }
            //}
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
        //public void GetShow()
        //{
        //    l1.Attributes["class"] = "control-label col-md-2  getshow";
        //    l11.Attributes["class"] = "control-label col-md-2  gethide";
        //    lbl12.Attributes["class"] = "control-label col-md-4  getshow";
        //    lbl13.Attributes["class"] = "control-label col-md-4  gethide";
        //    lbl1.Attributes["class"] = "control-label col-md-2  gethide";
        //    lbl2.Attributes["class"] = "control-label col-md-2  getshow";
        //    lbl3.Attributes["class"] = "control-label col-md-2  gethide";
        //    Label10.Attributes["class"] = "control-label col-md-2  getshow";
        //    lbl21.Attributes["class"] = "control-label col-md-4  gethide";
        //    Label24.Attributes["class"] = "control-label col-md-4  getshow";
        //    lbl6.Attributes["class"] = "control-label col-md-4  gethide";
        //    Label26.Attributes["class"] = "control-label col-md-4  getshow";
        //    lbl7.Attributes["class"] = "control-label col-md-4  gethide";
        //    Label28.Attributes["class"] = "control-label col-md-4  getshow";
        //    lbl9.Attributes["class"] = "control-label col-md-4  gethide";
        //    Label30.Attributes["class"] = "control-label col-md-4  getshow";
        //    lbl14.Attributes["class"] = "control-label col-md-4  gethide";
        //    Label32.Attributes["class"] = "control-label col-md-4  getshow";
        //    lbl87.Attributes["class"] = "control-label col-md-4  gethide";
        //    Label34.Attributes["class"] = "control-label col-md-4  getshow";
        //    lbl74.Attributes["class"] = "control-label col-md-4  gethide";
        //    Label36.Attributes["class"] = "control-label col-md-4  getshow";
        //    lbl22.Attributes["class"] = "control-label col-md-2  gethide";
        //    Label38.Attributes["class"] = "control-label col-md-2  getshow";
        //    lbl55.Attributes["class"] = "control-label col-md-2  gethide";
        //    Label40.Attributes["class"] = "control-label col-md-2  getshow";
        //    lbl65.Attributes["class"] = "control-label col-md-2  gethide";
        //    Label42.Attributes["class"] = "control-label col-md-2  getshow";
        //    lbl75.Attributes["class"] = "control-label col-md-2  gethide";
        //    Label44.Attributes["class"] = "control-label col-md-2  getshow";
        //    lbl78.Attributes["class"] = "control-label col-md-2  gethide";
        //    Label46.Attributes["class"] = "control-label col-md-2  getshow";
        //    lbl79.Attributes["class"] = "control-label col-md-2  gethide";
        //    Label48.Attributes["class"] = "control-label col-md-2  getshow";
        //    lbl70.Attributes["class"] = "control-label col-md-2  gethide";
        //    Label51.Attributes["class"] = "control-label col-md-2  getshow";
        //    lbl121.Attributes["class"] = "control-label col-md-2  gethide";
        //    Label52.Attributes["class"] = "control-label col-md-2  getshow";
        //    lbl456.Attributes["class"] = "control-label col-md-2  gethide";
        //    Label54.Attributes["class"] = "control-label col-md-2  getshow";
        //    lbl147.Attributes["class"] = "control-label col-md-4  gethide";
        //    Label56.Attributes["class"] = "control-label col-md-4  getshow";
        //    lbl564.Attributes["class"] = "control-label col-md-4  gethide";
        //    Label58.Attributes["class"] = "control-label col-md-4  getshow";
        //    lbl741.Attributes["class"] = "control-label col-md-4  gethide";
        //    Label60.Attributes["class"] = "control-label col-md-4  getshow";
        //    //lbl562.Attributes["class"] = "control-label col-md-4  gethide";
        //    //Label62.Attributes["class"] = "control-label col-md-4  getshow";
        //    lblrem123.Attributes["class"] = "control-label col-md-2  gethide";
        //    Label64.Attributes["class"] = "control-label col-md-2  getshow";
        //    lbl72.Attributes["class"] = "control-label col-md-4  gethide";
        //    Label66.Attributes["class"] = "control-label col-md-4  getshow";
        //    lbl521.Attributes["class"] = "control-label col-md-4  gethide";
        //    Label68.Attributes["class"] = "control-label col-md-4  getshow";
        //    lbl7521.Attributes["class"] = "control-label col-md-4  gethide";
        //    Label70.Attributes["class"] = "control-label col-md-4  getshow";
        //    lbl632.Attributes["class"] = "control-label col-md-4  gethide";
        //    Label72.Attributes["class"] = "control-label col-md-4  getshow";
        //    lbl5214.Attributes["class"] = "control-label col-md-4  gethide";
        //    Label74.Attributes["class"] = "control-label col-md-4  getshow";
        //}
        //public void GetHide()
        //{
        //    Label74.Attributes["class"] = "control-label col-md-4  gethide";
        //    lbl5214.Attributes["class"] = "control-label col-md-4  getshow";
        //    Label72.Attributes["class"] = "control-label col-md-4  gethide";
        //    lbl632.Attributes["class"] = "control-label col-md-4  getshow";
        //    Label70.Attributes["class"] = "control-label col-md-4  gethide";
        //    lbl7521.Attributes["class"] = "control-label col-md-4  getshow";
        //    Label68.Attributes["class"] = "control-label col-md-4  gethide";
        //    lbl521.Attributes["class"] = "control-label col-md-4  getshow";
        //    Label66.Attributes["class"] = "control-label col-md-4  gethide";
        //    lbl72.Attributes["class"] = "control-label col-md-4  getshow";
        //    Label64.Attributes["class"] = "control-label col-md-2  gethide";
        //    lblrem123.Attributes["class"] = "control-label col-md-2  getshow";
        //    //Label62.Attributes["class"] = "control-label col-md-4  gethide";
        //    //lbl562.Attributes["class"] = "control-label col-md-4  getshow";
        //    Label60.Attributes["class"] = "control-label col-md-4  gethide";
        //    lbl741.Attributes["class"] = "control-label col-md-4  getshow";
        //    Label58.Attributes["class"] = "control-label col-md-4  gethide";
        //    lbl564.Attributes["class"] = "control-label col-md-4  getshow";
        //    Label56.Attributes["class"] = "control-label col-md-4  gethide";
        //    lbl147.Attributes["class"] = "control-label col-md-4  getshow";
        //    Label54.Attributes["class"] = "control-label col-md-2  gethide";
        //    lbl456.Attributes["class"] = "control-label col-md-2  getshow";
        //    Label52.Attributes["class"] = "control-label col-md-2  gethide";
        //    lbl121.Attributes["class"] = "control-label col-md-2  getshow";
        //    Label51.Attributes["class"] = "control-label col-md-2  gethide";
        //    lbl70.Attributes["class"] = "control-label col-md-2  getshow";
        //    Label48.Attributes["class"] = "control-label col-md-2  gethide";
        //    lbl79.Attributes["class"] = "control-label col-md-2  getshow";
        //    Label46.Attributes["class"] = "control-label col-md-2  gethide";
        //    lbl78.Attributes["class"] = "control-label col-md-2  getshow";
        //    Label44.Attributes["class"] = "control-label col-md-2  gethide";
        //    lbl75.Attributes["class"] = "control-label col-md-2  getshow";
        //    Label42.Attributes["class"] = "control-label col-md-2  gethide";
        //    lbl65.Attributes["class"] = "control-label col-md-2  getshow";
        //    Label40.Attributes["class"] = "control-label col-md-2  gethide";
        //    lbl55.Attributes["class"] = "control-label col-md-2  getshow";
        //    Label38.Attributes["class"] = "control-label col-md-2  gethide";
        //    lbl22.Attributes["class"] = "control-label col-md-2  getshow";
        //    Label36.Attributes["class"] = "control-label col-md-4  gethide";
        //    lbl74.Attributes["class"] = "control-label col-md-4  getshow";
        //    Label34.Attributes["class"] = "control-label col-md-4  gethide";
        //    lbl87.Attributes["class"] = "control-label col-md-4  getshow";
        //    Label32.Attributes["class"] = "control-label col-md-4  gethide";
        //    lbl14.Attributes["class"] = "control-label col-md-4  getshow";
        //    Label30.Attributes["class"] = "control-label col-md-4  gethide";
        //    lbl9.Attributes["class"] = "control-label col-md-4  getshow";
        //    Label28.Attributes["class"] = "control-label col-md-4  gethide";
        //    lbl7.Attributes["class"] = "control-label col-md-4  getshow";
        //    l1.Attributes["class"] = "control-label col-md-2  gethide";
        //    l11.Attributes["class"] = "control-label col-md-2  getshow";
        //    lbl12.Attributes["class"] = "control-label col-md-4  gethide";
        //    lbl13.Attributes["class"] = "control-label col-md-4  getshow";
        //    lbl1.Attributes["class"] = "control-label col-md-2  getshow";
        //    lbl3.Attributes["class"] = "control-label col-md-2  getshow";
        //    lbl2.Attributes["class"] = "control-label col-md-2  gethide";
        //    Label10.Attributes["class"] = "control-label col-md-2  gethide";
        //    lbl21.Attributes["class"] = "control-label col-md-4  getshow";
        //    Label24.Attributes["class"] = "control-label col-md-4  gethide";
        //    lbl6.Attributes["class"] = "control-label col-md-4  getshow";
        //    Label26.Attributes["class"] = "control-label col-md-4  gethide";
        //}
        public void redonlyfalse()
        {
            txtAddress.Enabled = false;
            txtcompneySerch.Enabled = false;
            LinkButton12.Enabled = false;
            txtAddress2.Enabled = txtCity.Enabled = txtcPassword.Enabled = txtcUserName.Enabled = txtCustomer.Enabled = txtCustomer2.Enabled = txtCustomerName.Enabled = txtMobileNo.Enabled = txtPostalCode.Enabled = txtRemark.Enabled = txtSocial.Enabled = txtWebsite.Enabled = txtZipCode.Enabled = false;
            drpCategory.Enabled = drpbrand.Enabled = drpCompnay.Enabled = drpCountry.Enabled = drpItManager.Enabled = drpMyCounLocID.Enabled = drpMyProductId.Enabled = drpPrimaryLang.Enabled = drpSomib.Enabled = drpSates.Enabled = drpType.Enabled = false;
            lblBusContactDe.Text = "Business Company Details -  (Read Mode)";
            lblBCDeta.Text = "Business Company Details -  (Read Mode)";
            lblwExistance.Text = "Web Existance -  (Read Mode)";
            lblWEmp.Text = "Working Employees -  (Read Mode)";
            ViewState["ModeData"] = "Read";
            lkbCustomerN1.Enabled = lkbCustomerN2.Enabled = lkbcompnyN3.Enabled = lbkEmail.Enabled = lkbFax.Enabled = lbkBusPhone.Enabled = lkbcheck.Enabled = libtnNewClass.Enabled = LinkButton6.Enabled = LinkButton14.Enabled = LinkButton4.Enabled = LinkButton7.Enabled = LinkButton13.Enabled = false;

        }
        public void redonlyture()
        {
            txtAddress.Enabled = true;
            txtAddress2.Enabled = txtCity.Enabled = txtcPassword.Enabled = txtcUserName.Enabled = txtCustomer.Enabled = txtCustomer2.Enabled = txtCustomerName.Enabled = txtMobileNo.Enabled = txtPostalCode.Enabled = txtRemark.Enabled = txtSocial.Enabled = txtWebsite.Enabled = txtZipCode.Enabled = true;
            drpCategory.Enabled = drpbrand.Enabled = drpCompnay.Enabled = drpCountry.Enabled = drpItManager.Enabled = drpMyCounLocID.Enabled = drpMyProductId.Enabled = drpPrimaryLang.Enabled = drpSomib.Enabled = drpSates.Enabled = drpType.Enabled = true;
            txtcompneySerch.Enabled = true;
            LinkButton12.Enabled = true;
            lblBusContactDe.Text = "Business Company Details - (Write Mode)";
            lblBCDeta.Text = "Business Company Details - (Write Mode)";
            lblwExistance.Text = "Web Existance -  (Write Mode)";
            lblWEmp.Text = "Working Employees - (Write Mode)";
            ViewState["ModeData"] = "Write";
            lkbCustomerN1.Enabled = lkbCustomerN2.Enabled = lkbcompnyN3.Enabled = lbkEmail.Enabled = lkbFax.Enabled = lbkBusPhone.Enabled = lkbcheck.Enabled = libtnNewClass.Enabled = LinkButton6.Enabled = LinkButton14.Enabled = LinkButton4.Enabled = LinkButton7.Enabled = LinkButton13.Enabled = true;
        }
        //for localization
     
        //for localization
        
        public void binddata1()
        {
            //Listview1.DataSource = DB.TBLCOMPANYSETUPs.Where(p => p.TenentID == TID && p.Approved == 1).Take(5);
            //Listview1.DataBind();
        }
        public void BindData()
        {
            if (ViewState["SerchListofCompny"] == null)
            {

                List<TBLCOMPANYSETUP> List = DB.TBLCOMPANYSETUPs.Where(p => p.TenentID == TID && p.Active == "Y").ToList();
                ViewState["SaveList"] = List;
                //int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
                //int Totalrec = List.Count();
                //((CRMMaster)Page.Master).Loadlist(Showdata, take, Skip, ChoiceID, lblShowinfEntry, btnPrevious1, btnNext1, Listview1, ListView2, Totalrec, List);
                Listview1.DataSource = List;
                Listview1.DataBind();

            }

            else
            {
                
                var List = ((List<Database.TBLCOMPANYSETUP>)ViewState["SerchListofCompny"]).ToList();
               
               
                ViewState["SaveList"] = List;
                ViewState["SerchListofCompny"] = List;
                //int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
                //int Totalrec = List.Count();
                //((CRMMaster)Page.Master).Loadlist(Showdata, take, Skip, ChoiceID, lblShowinfEntry, btnPrevious1, btnNext1, Listview1, ListView2, Totalrec, List);
                Listview1.DataSource = List;//DB.TBLCONTACT.Where(p => p.TenentID == TID && p.ContactMyID==TitleID && p.Active == "Y" && p.PHYSICALLOCID != "HLY");
                Listview1.DataBind();
                
            }
            //int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
            //int Totalrec = List.Count();
            //((CRMMaster)Page.Master).Loadlist(Showdata, take, Skip, ChoiceID, lblShowinfEntry, btnPrevious1, btnNext1, Listview1, ListView2, Totalrec, List);
        }
        //public void BindData()
        //{

        //}
        public string getremark(int RID)
        {
            return DB.REFTABLEs.SingleOrDefault(p => p.REFID == RID && p.TenentID == TID).Remarks;
        }
        public void SoiclMediya(int CID)
        {
            listSocialMedia.DataSource = DB.Tbl_RecordType_Mst.Where(p => p.TenentID == TID && p.CompniyAndContactID == CID && p.Active == true && p.RecordType == "socialmedia");
            listSocialMedia.DataBind();
        }
        public void GridBind()
        {
            //GridView1.DataSource = DB.REFTABLE.Where(p => p.REFTYPE == "INDUSTRY" && p.REFSUBTYPE == "LIST");
            //GridView1.DataBind();
        }
        public void clen()
        {
            lblcountserch.Text = txtcompneySerch.Text = lblCustomerName.Text = lblCustomerL1.Text = lblCustomerL2.Text = lblEmail12.Text = Label21.Text = lblMobileNo.Text = txtAddress.Text = txtAddress2.Text = txtCity.Text = txtcPassword.Text = txtcUserName.Text = txtCustomer.Text = txtCustomer2.Text = txtCustomerName.Text = txtMobileNo.Text = txtPostalCode.Text = tags_5.Text = txtRemark.Text = txtWebsite.Text = txtZipCode.Text = tags_1.Text = tags_2.Text = tags_3.Text = tags_4.Text = "";
            drpCategory.SelectedIndex = 0;
            chbIsMinistry.Checked = chbIssMb.Checked = chbIsCorporate.Checked = chbInHawally.Checked = chbSaler.Checked = chbBuyer.Checked = chbSaleDeProd.Checked = chbEmailSub.Checked = false;
            drpSates.SelectedIndex = 0;
            drpItManager.SelectedIndex = 0;
            drpMyCounLocID.SelectedIndex = 0;
            drpMyProductId.SelectedIndex = 0;
            drpPrimaryLang.SelectedIndex = 0;
            //drpState.SelectedIndex = 0;
            drpType.SelectedIndex = 0;
            drpCountry.SelectedIndex = 0;

        }
        protected void ListCustomerMaster_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (e.CommandName == "btnDelete")
            {
                int ID = Convert.ToInt32(e.CommandArgument);
                Database.TBLCOMPANYSETUP objtbl_COMPANYSETUP = DB.TBLCOMPANYSETUPs.Single(p => p.COMPID == ID && p.TenentID == TID);
                objtbl_COMPANYSETUP.Active = "N";
                //objtbl_COMPANYSETUP.Approved = 1;
                DB.SaveChanges();
                BindData();

            }
            if (e.CommandName == "btnview")
            {

                int COMPID = Convert.ToInt32(Request.QueryString["COMPID"]);
                BindEditcompniy(COMPID);
                Session["ADMInPrevious"] = Session["ADMInCurrent"];
                Session["ADMInCurrent"] = Request.RawUrl;
                redonlyfalse();
                lblBusContactDe.Text = "Business Company Details -  (Read Mode)";
                lblBCDeta.Text = "Business Company Details -  (Read Mode)";
                lblwExistance.Text = "Web Existance -  (Read Mode)";
                lblWEmp.Text = "Working Employees -  (Read Mode)";
                btnattmentmst.Visible = true;
                lblAttecmentcount.Text = Countatt(COMPID);

            }
            if (e.CommandName == "btnEdit")
            {

                int COMPID = Convert.ToInt32(Request.QueryString["COMPID"]);
                BindEditcompniy(COMPID);
                Session["ADMInPrevious"] = Session["ADMInCurrent"];
                Session["ADMInCurrent"] = Request.RawUrl;
                redonlyture();
                lblBusContactDe.Text = "Business Company Details -Edit Mode";
                lblBCDeta.Text = "Business Company Details -Edit Mode";
                lblwExistance.Text = "Web Existance -Edit Mode";
                lblWEmp.Text = "Working Employees -Edit Mode";
                btnSubmit.Visible = true;
                Button1.Visible = false;
                btnSubmit.Text = "Update";
                btnattmentmst.Visible = true;
                lblAttecmentcount.Text = Countatt(COMPID);
            }

        }
        public string Countatt(int COMPID)
        {
            var List = DB.tbl_DMSAttachmentMst.Where(p => p.AttachmentById == COMPID && p.AttachmentByName == "CompanyMaster"&&p.TenentID==TID).ToList();
            int Count = List.Count();
            return Count.ToString();
        }
        public void Lastdataredmode()
        {
            btnSubmit.Visible = false;
            DateTime Maxdate = Convert.ToDateTime(DB.TBLCOMPANYSETUPs.Where(p => p.TenentID == TID).Max(p => p.UPDTTIME));
            if (DB.TBLCOMPANYSETUPs.Where(p => p.TenentID == TID && p.UPDTTIME == Maxdate).Count() > 0)
            {
                int CMPNID = DB.TBLCOMPANYSETUPs.Where(p => p.TenentID == TID && p.UPDTTIME == Maxdate).Max(p => p.COMPID);
                BindEditcompniy(CMPNID);
            }

        }
        public void Firstdataredmode()
        {
            btnSubmit.Visible = false;
            // DateTime Maxdate = Convert.ToDateTime(DB.TBLCOMPANYSETUPs.Where(p => p.TenentID == TID).Max(p => p.UPDTTIME));
            if (DB.TBLCOMPANYSETUPs.Where(p => p.TenentID == TID).Count() > 0)
            {
                int CMPNID = Convert.ToInt32(DB.TBLCOMPANYSETUPs.FirstOrDefault(p => p.TenentID == TID && p.Active == "Y").COMPID);
                BindEditcompniy(CMPNID);
            }

        }
        public void BindEditcompniy(int ID)
        {
            Database.TBLCOMPANYSETUP objtbl_COMPANYSETUP = DB.TBLCOMPANYSETUPs.Single(p => p.TenentID == TID && p.COMPID == ID && p.Active == "Y");

            //int CID = Convert.ToInt32(DB.CRM_tblCOUNTRY.Single(p => p.COUNTRYID == objtbl_COMPANYSETUP.COUNTRYID).COUNTRYID);
            //drpState.DataSource = DB.CRM_tblStates.Where(p => p.COUNTRYID == objtbl_COMPANYSETUP.COUNTRYID && p.ACTIVE == "Y");
            //drpState.DataTextField = "MYNAME1";
            //drpState.DataValueField = "StateID";
            //drpState.DataBind();
            //drpState.Items.Insert(0, new ListItem("-- Select --", "0"));

            btnSubmit.Text = "Update";
            txtCustomerName.Text = objtbl_COMPANYSETUP.COMPNAME1;
            txtCustomer.Text = objtbl_COMPANYSETUP.COMPNAME2;
            txtCustomer2.Text = objtbl_COMPANYSETUP.COMPNAME3;
            tags_2.Text = objtbl_COMPANYSETUP.EMAIL1;
            //if (objtbl_COMPANYSETUP.ITMANAGER != null || objtbl_COMPANYSETUP.ITMANAGER != "0")
            //{
            //    drpItManager.SelectedValue = objtbl_COMPANYSETUP.ITMANAGER;
            //}

            txtAddress.Text = objtbl_COMPANYSETUP.ADDR1;
            txtAddress2.Text = objtbl_COMPANYSETUP.ADDR2;
            txtCity.Text = objtbl_COMPANYSETUP.CITY;

            txtPostalCode.Text = objtbl_COMPANYSETUP.POSTALCODE;
            txtZipCode.Text = objtbl_COMPANYSETUP.ZIPCODE;
            drpMyCounLocID.SelectedValue = objtbl_COMPANYSETUP.MYCONLOCID.ToString();
            if (objtbl_COMPANYSETUP.MYPRODID != null && objtbl_COMPANYSETUP.MYPRODID != 0)
                drpMyProductId.SelectedValue = objtbl_COMPANYSETUP.MYPRODID.ToString();

            tags_4.Text = objtbl_COMPANYSETUP.BUSPHONE1;
            txtMobileNo.Text = objtbl_COMPANYSETUP.MOBPHONE;
            tags_3.Text = objtbl_COMPANYSETUP.FAX;
            if (objtbl_COMPANYSETUP.PRIMLANGUGE != null && objtbl_COMPANYSETUP.PRIMLANGUGE != "0" && objtbl_COMPANYSETUP.PRIMLANGUGE != "0")
            {
                int PLID = Convert.ToInt32(objtbl_COMPANYSETUP.PRIMLANGUGE);
                drpPrimaryLang.SelectedValue = DB.tblLanguages.Single(p => p.MYCONLOCID == PLID && p.TenentID == TID).MYCONLOCID.ToString();
            }

            txtWebsite.Text = objtbl_COMPANYSETUP.WEBPAGE;
            if (objtbl_COMPANYSETUP.CompanyType != null && Convert.ToInt32(objtbl_COMPANYSETUP.CompanyType) != 0)
            {
                drpType.SelectedValue = (objtbl_COMPANYSETUP.CompanyType).ToString();
            }

            chbIsMinistry.Checked = (objtbl_COMPANYSETUP.ISMINISTRY == true) ? true : false;
            chbIssMb.Checked = (objtbl_COMPANYSETUP.ISSMB == true) ? true : false;
            chbIsCorporate.Checked = (objtbl_COMPANYSETUP.ISCORPORATE == true) ? true : false;
            chbInHawally.Checked = (objtbl_COMPANYSETUP.INHAWALLY == true) ? true : false;
            chbSaler.Checked = (objtbl_COMPANYSETUP.SALER == true) ? true : false;
            chbBuyer.Checked = (objtbl_COMPANYSETUP.BUYER == true) ? true : false;
            chbSaleDeProd.Checked = (objtbl_COMPANYSETUP.SALEDEPROD == true) ? true : false;
            chbEmailSub.Checked = (objtbl_COMPANYSETUP.EMAISUB == true) ? true : false;
            tags_5.Text = objtbl_COMPANYSETUP.PRODUCTDEALIN;
            txtRemark.Text = objtbl_COMPANYSETUP.REMARKS;
            tags_1.Text = objtbl_COMPANYSETUP.Keyword;
            txtcUserName.Text = objtbl_COMPANYSETUP.CUSERID;
            txtcPassword.Text = objtbl_COMPANYSETUP.CPASSWRD;

            string COID = objtbl_COMPANYSETUP.COUNTRYID.ToString();
            drpCountry.SelectedValue = COID.ToString();
            bindSates(COID);
            if (objtbl_COMPANYSETUP.STATE != null && objtbl_COMPANYSETUP.STATE != "0")
            {
                drpSates.SelectedValue = objtbl_COMPANYSETUP.STATE;
            }
            SoiclMediya(ID);
            BindConmpnicontect(ID);
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {

            using (var scope = new System.Transactions.TransactionScope())
            {
                //try
                //{
                string UID = ((USER_MST)Session["USER"]).LOGIN_ID;
                int CID = Convert.ToInt32(ViewState["compId"]);

                if (Request.QueryString["COMPID"] != null || CID != 0)
                {
                    Database.TBLCOMPANYSETUP objtbl_COMPANYSETUP;
                    int COMPID = Convert.ToInt32(Request.QueryString["COMPID"]);
                    if (COMPID != null && COMPID != 0)
                    {
                        objtbl_COMPANYSETUP = DB.TBLCOMPANYSETUPs.Single(p => p.TenentID == TID && p.COMPID == COMPID);

                    }
                    else
                    {
                        objtbl_COMPANYSETUP = DB.TBLCOMPANYSETUPs.Single(p => p.TenentID == TID && p.COMPID == CID);
                    }

                    objtbl_COMPANYSETUP.TenentID = TID;
                    objtbl_COMPANYSETUP.PHYSICALLOCID = txtCustomerName.Text;
                    objtbl_COMPANYSETUP.COMPNAME1 = txtCustomerName.Text;
                    objtbl_COMPANYSETUP.COMPNAME2 = txtCustomer.Text;
                    objtbl_COMPANYSETUP.COMPNAME3 = txtCustomer2.Text;
                    objtbl_COMPANYSETUP.EMAIL1 = tags_2.Text;
                    // objtbl_COMPANYSETUP.EMAIL2 = tags_2.Text;
                    objtbl_COMPANYSETUP.ITMANAGER = drpItManager.SelectedValue;
                    objtbl_COMPANYSETUP.ADDR1 = txtAddress.Text;
                    objtbl_COMPANYSETUP.ADDR2 = txtAddress2.Text;
                    objtbl_COMPANYSETUP.CITY = txtCity.Text;
                    objtbl_COMPANYSETUP.STATE = drpSates.SelectedValue;
                    objtbl_COMPANYSETUP.POSTALCODE = txtPostalCode.Text;
                    objtbl_COMPANYSETUP.ZIPCODE = txtZipCode.Text;
                    objtbl_COMPANYSETUP.MYCONLOCID = Convert.ToInt32(drpMyCounLocID.SelectedValue);
                    objtbl_COMPANYSETUP.MYPRODID = Convert.ToInt32(drpMyProductId.SelectedValue);
                    //   string CNAME = drpCountry.SelectedItem.ToString();
                    objtbl_COMPANYSETUP.COUNTRYID = Convert.ToInt32(drpCountry.SelectedValue);
                    objtbl_COMPANYSETUP.BUSPHONE1 = tags_4.Text;
                    //objtbl_COMPANYSETUP.BUSPHONE2 = tags_4.Text;
                    //objtbl_COMPANYSETUP.BUSPHONE3 = tags_4.Text;
                    //objtbl_COMPANYSETUP.BUSPHONE4 = tags_4.Text;
                    objtbl_COMPANYSETUP.MOBPHONE = txtMobileNo.Text;
                    objtbl_COMPANYSETUP.FAX = tags_3.Text;
                    //  objtbl_COMPANYSETUP.FAX2 = tags_3.Text;
                    objtbl_COMPANYSETUP.PRIMLANGUGE = drpPrimaryLang.SelectedValue;
                    objtbl_COMPANYSETUP.WEBPAGE = txtWebsite.Text;
                    objtbl_COMPANYSETUP.CompanyType = drpType.SelectedValue;
                    objtbl_COMPANYSETUP.ISMINISTRY = chbIsMinistry.Checked ? true : false;
                    objtbl_COMPANYSETUP.ISSMB = chbIssMb.Checked ? true : false;
                    objtbl_COMPANYSETUP.ISCORPORATE = chbIsCorporate.Checked ? true : false;
                    objtbl_COMPANYSETUP.INHAWALLY = chbInHawally.Checked ? true : false;
                    objtbl_COMPANYSETUP.SALER = chbSaler.Checked ? true : false;
                    objtbl_COMPANYSETUP.BUYER = chbBuyer.Checked ? true : false;
                    objtbl_COMPANYSETUP.SALEDEPROD = chbSaleDeProd.Checked ? true : false;
                    objtbl_COMPANYSETUP.EMAISUB = chbEmailSub.Checked ? true : false;
                    objtbl_COMPANYSETUP.EMAILSUBDATE = DateTime.Now;
                    objtbl_COMPANYSETUP.PRODUCTDEALIN = tags_5.Text;
                    objtbl_COMPANYSETUP.REMARKS = txtRemark.Text;
                    objtbl_COMPANYSETUP.Keyword = tags_1.Text;
                    objtbl_COMPANYSETUP.CUSERID = txtcUserName.Text;
                    objtbl_COMPANYSETUP.CPASSWRD = txtcPassword.Text;
                    objtbl_COMPANYSETUP.USERID = UID;
                    //objtbl_COMPANYSETUP.ENTRYDATE = DateTime.Now;
                    //objtbl_COMPANYSETUP.ENTRYTIME = DateTime.Now;
                    objtbl_COMPANYSETUP.UPDTTIME = DateTime.Now;
                    objtbl_COMPANYSETUP.Approved = 0;
                    DB.SaveChanges();

                    // COMPNY CLASSIFICATION
                    if (tags_1.Text != "")
                    {
                        List<Tbl_RecordType_Mst> List4;
                        if (COMPID != null && COMPID != 0)
                            List4 = DB.Tbl_RecordType_Mst.Where(p => p.CompniyAndContactID == COMPID && p.RecordType == "Classification" && p.Recource == 5005&&p.TenentID==TID).ToList();

                        else
                            List4 = DB.Tbl_RecordType_Mst.Where(p => p.CompniyAndContactID == CID && p.RecordType == "Classification" && p.Recource == 5005&&p.TenentID==TID).ToList();



                        foreach (Database.Tbl_RecordType_Mst item in List4)
                        {
                            //var Deleteobj = DB.Tbl_RecordType_Mst.Single(p => p.CompniyID == item.CompniyID && p.RecTypeID == item.RecTypeID && p.RecordType == item .RecordType );
                            DB.Tbl_RecordType_Mst.DeleteObject(item);

                            DB.SaveChanges();
                        }

                        string[] Seperate4 = tags_1.Text.Split(',');
                        int count4 = 0;
                        string Sep4 = "";
                        for (int i = 0; i <= Seperate4.Count() - 1; i++)
                        {
                            Sep4 = Seperate4[i];

                            count4++;
                            Database.Tbl_RecordType_Mst obj = new Database.Tbl_RecordType_Mst();
                            obj.TenentID = TID;
                            obj.RecordType = "Classification";
                            obj.RecTypeID = DB.Tbl_RecordType_Mst.Where(p => p.TenentID == TID).Count() > 0 ? Convert.ToInt32(DB.Tbl_RecordType_Mst.Where(p => p.TenentID == TID).Max(p => p.RecTypeID) + 1) : 1;
                            obj.CompniyAndContactID = Convert.ToInt32(objtbl_COMPANYSETUP.COMPID);
                            obj.RunSerial = count4;
                            obj.Recource = 5005;
                            obj.RecourceName = "Company";
                            obj.RecValue = Sep4;
                            obj.Active = true;
                            // obj.Rremark = "AutomatedProcess";
                            DB.Tbl_RecordType_Mst.AddObject(obj);
                            DB.SaveChanges();
                        }
                    }

                    // Brand
                    if (tags_5.Text != "")
                    {
                        List<Tbl_RecordType_Mst> List6;
                        if (COMPID != null && COMPID != 0)
                            List6 = DB.Tbl_RecordType_Mst.Where(p => p.CompniyAndContactID == COMPID && p.RecordType == "BRAND" && p.TenentID == TID && p.Recource == 5005 && p.TenentID == TID).ToList();

                        else
                            List6 = DB.Tbl_RecordType_Mst.Where(p => p.CompniyAndContactID == CID && p.RecordType == "BRAND" && p.TenentID == TID && p.Recource == 5005 && p.TenentID == TID).ToList();



                        foreach (Database.Tbl_RecordType_Mst item in List6)
                        {
                            //var Deleteobj = DB.Tbl_RecordType_Mst.Single(p => p.CompniyID == item.CompniyID && p.RecTypeID == item.RecTypeID && p.RecordType == item .RecordType );
                            DB.Tbl_RecordType_Mst.DeleteObject(item);

                            DB.SaveChanges();
                        }
                        string[] Seperate5 = tags_5.Text.Split(',');
                        int count5 = 0;
                        string Sep5 = "";
                        for (int i = 0; i <= Seperate5.Count() - 1; i++)
                        {
                            Sep5 = Seperate5[i];

                            count5++;
                            Tbl_RecordType_Mst obj = new Tbl_RecordType_Mst();
                            obj.TenentID = TID;
                            obj.RecordType = "BRAND";
                            obj.RecTypeID = DB.Tbl_RecordType_Mst.Where(p => p.TenentID == TID).Count() > 0 ? Convert.ToInt32(DB.Tbl_RecordType_Mst.Where(p => p.TenentID == TID).Max(p => p.RecTypeID) + 1) : 1;
                            obj.CompniyAndContactID = objtbl_COMPANYSETUP.COMPID;
                            obj.RunSerial = count5;
                            obj.Recource = 5005;
                            obj.RecourceName = "Company";
                            obj.RecValue = Seperate5[i];
                            obj.Active = true;
                            // obj.Rremark = "AutomatedProcess";
                            DB.Tbl_RecordType_Mst.AddObject(obj);
                            DB.SaveChanges();
                        }
                    }

                    if (tags_2.Text != "")
                    {
                        List<Tbl_RecordType_Mst> List5;
                        if (COMPID != null && COMPID != 0)
                            List5 = DB.Tbl_RecordType_Mst.Where(p => p.TenentID == TID && p.CompniyAndContactID == COMPID && p.RecordType == "Email" && p.Recource == 5005 && p.TenentID == TID).ToList();

                        else
                            List5 = DB.Tbl_RecordType_Mst.Where(p => p.TenentID == TID && p.CompniyAndContactID == CID && p.RecordType == "Email" && p.Recource == 5005 && p.TenentID == TID).ToList();



                        foreach (Database.Tbl_RecordType_Mst item in List5)
                        {
                            // var Deleteobj = DB.Tbl_RecordType_Mst.SingleOrDefault(p => p.CompniyID == COMPID && p.RecTypeID == 5001 && p.RecordType == "Email");
                            DB.Tbl_RecordType_Mst.DeleteObject(item);
                            DB.SaveChanges();
                        }
                        string[] Seperate = tags_2.Text.Split(',');
                        int count = 0;
                        string Sep = "";
                        for (int i = 0; i <= Seperate.Count() - 1; i++)
                        {
                            Sep = Seperate[i];
                            var exist = DB.Tbl_RecordType_Mst.Where(c => c.RecValue == Sep && c.RecordType == "Email" && c.TenentID == TID && c.Recource == 5005);
                            if (exist.Count() <= 0)
                            {
                                count++;
                                Tbl_RecordType_Mst obj = new Tbl_RecordType_Mst();
                                obj.TenentID = TID;
                                obj.RecordType = "Email";
                                obj.RecTypeID = DB.Tbl_RecordType_Mst.Where(p => p.TenentID == TID).Count() > 0 ? Convert.ToInt32(DB.Tbl_RecordType_Mst.Where(p => p.TenentID == TID).Max(p => p.RecTypeID) + 1) : 1;
                                obj.CompniyAndContactID = objtbl_COMPANYSETUP.COMPID;
                                obj.RunSerial = count;
                                obj.Recource = 5005;
                                obj.RecourceName = "Company";
                                obj.RecValue = Seperate[i];
                                obj.Active = true;
                                // obj.Rremark = "AutomatedProcess";
                                DB.Tbl_RecordType_Mst.AddObject(obj);
                                DB.SaveChanges();
                            }

                            else
                            {
                                string display = "Email Is Duplicate!";
                                ClientScript.RegisterStartupScript(this.GetType(), "Email Is Duplicate!", "alert('" + display + "');", true);
                                return;
                            }
                        }

                    }
                    // email Edit 
                    if (tags_3.Text != "")
                    {
                        // Edit Fax

                        List<Tbl_RecordType_Mst> List1;
                        if (COMPID != null && COMPID != 0)
                            List1 = DB.Tbl_RecordType_Mst.Where(p => p.TenentID == TID && p.CompniyAndContactID == COMPID && p.Recource == 5005 && p.RecordType == "Fax").ToList();

                        else
                            List1 = DB.Tbl_RecordType_Mst.Where(p => p.TenentID == TID && p.CompniyAndContactID == CID && p.Recource == 5005 && p.RecordType == "Fax").ToList();


                        foreach (Database.Tbl_RecordType_Mst item in List1)
                        {

                            DB.Tbl_RecordType_Mst.DeleteObject(item);
                            DB.SaveChanges();
                        }

                        string[] Seperate1 = tags_3.Text.Split(',');
                        int count1 = 0;
                        string Sep1 = "";
                        for (int i = 0; i <= Seperate1.Count() - 1; i++)
                        {
                            Sep1 = Seperate1[i];
                            var exist = DB.Tbl_RecordType_Mst.Where(c => c.RecValue == Sep1 && c.RecordType == "Fax" && c.TenentID == TID && c.Recource == 5005);
                            if (exist.Count() <= 0)
                            {
                                count1++;
                                Tbl_RecordType_Mst obj = new Tbl_RecordType_Mst();
                                obj.TenentID = TID;
                                obj.RecordType = "Fax";
                                obj.RecTypeID = DB.Tbl_RecordType_Mst.Where(p => p.TenentID == TID).Count() > 0 ? Convert.ToInt32(DB.Tbl_RecordType_Mst.Where(p => p.TenentID == TID).Max(p => p.RecTypeID) + 1) : 1;
                                obj.CompniyAndContactID = objtbl_COMPANYSETUP.COMPID;
                                obj.RunSerial = count1;
                                obj.Recource = 5005;
                                obj.RecourceName = "Company";
                                obj.RecValue = Seperate1[i];
                                obj.Active = true;
                                // obj.Rremark = "AutomatedProcess";
                                DB.Tbl_RecordType_Mst.AddObject(obj);
                                DB.SaveChanges();

                            }
                            else
                            {
                                string display = "Fax Number Is Duplicate!";
                                ClientScript.RegisterStartupScript(this.GetType(), "Fax Number Is Duplicate!", "alert('" + display + "');", true);
                                return;
                            }
                        }
                    }

                    if (tags_4.Text != "")
                    {
                        //BusPhonuber
                        List<Tbl_RecordType_Mst> List2;
                        if (COMPID != null && COMPID != 0)
                            List2 = DB.Tbl_RecordType_Mst.Where(p => p.TenentID == TID && p.CompniyAndContactID == COMPID && p.Recource == 5005 && p.RecordType == "BusPhone").ToList();

                        else
                            List2 = DB.Tbl_RecordType_Mst.Where(p => p.TenentID == TID && p.CompniyAndContactID == CID && p.Recource == 5005 && p.RecordType == "BusPhone").ToList();

                        foreach (Database.Tbl_RecordType_Mst item in List2)
                        {
                            // var Deleteobj = DB.Tbl_RecordType_Mst.SingleOrDefault(p => p.CompniyID == COMPID && p.RecTypeID == 5003 && p.RecordType == "BusPhone");
                            DB.Tbl_RecordType_Mst.DeleteObject(item);
                            DB.SaveChanges();
                        }

                        string[] Seperate2 = tags_4.Text.Split(',');
                        int count2 = 0;
                        string Sep2 = "";
                        for (int i = 0; i <= Seperate2.Count() - 1; i++)
                        {
                            Sep2 = Seperate2[i];
                            var exist = DB.Tbl_RecordType_Mst.Where(c => c.RecValue == Sep2 && c.CompniyAndContactID == COMPID && c.RecordType == "BusPhone" && c.TenentID == TID && c.Recource == 5005);
                            if (exist.Count() <= 0)
                            {
                                count2++;
                                Tbl_RecordType_Mst obj = new Tbl_RecordType_Mst();
                                obj.TenentID = TID;
                                obj.RecordType = "BusPhone";
                                obj.RecTypeID = DB.Tbl_RecordType_Mst.Where(p => p.TenentID == TID).Count() > 0 ? Convert.ToInt32(DB.Tbl_RecordType_Mst.Where(p => p.TenentID == TID).Max(p => p.RecTypeID) + 1) : 1;
                                obj.CompniyAndContactID = objtbl_COMPANYSETUP.COMPID;
                                obj.RunSerial = count2;
                                obj.Recource = 5005;
                                obj.RecourceName = "Company";
                                obj.RecValue = Seperate2[i];
                                obj.Active = true;
                                // obj.Rremark = "AutomatedProcess";
                                DB.Tbl_RecordType_Mst.AddObject(obj);
                                DB.SaveChanges();

                            }
                            else
                            {
                                string display = "Bus Number Is Duplicate!";
                                ClientScript.RegisterStartupScript(this.GetType(), "Bus Number Is Duplicate!", "alert('" + display + "');", true);
                                return;
                            }
                        }
                    }

                    if (ViewState["ListtblCONTACTBus"] != null)
                    {
                        var Listbus = ((List<Database.tblCONTACTBu>)ViewState["ListtblCONTACTBus"]).ToList();
                        for (int i = 0; i < Listbus.Count(); i++)
                        {
                            Database.tblCONTACTBu objtbl_CONTACTBus = new Database.tblCONTACTBu();
                            objtbl_CONTACTBus.TenentID = Listbus[i].TenentID;
                            objtbl_CONTACTBus.BusPhone1 = Listbus[i].BusPhone1;
                            objtbl_CONTACTBus.ContactMyID = Listbus[i].ContactMyID;
                            objtbl_CONTACTBus.CompID = Listbus[i].CompID;
                            objtbl_CONTACTBus.Country = Listbus[i].Country;
                            objtbl_CONTACTBus.email2 = Listbus[i].email2;
                            objtbl_CONTACTBus.Fax = Listbus[i].Fax;
                            objtbl_CONTACTBus.JobTitle = Listbus[i].JobTitle;
                            objtbl_CONTACTBus.PhysicalLocID = Listbus[i].PhysicalLocID;
                            objtbl_CONTACTBus.remarks = Listbus[i].remarks;
                            objtbl_CONTACTBus.Active = Listbus[i].Active;
                            DB.tblCONTACTBus.AddObject(objtbl_CONTACTBus);
                            DB.SaveChanges();

                        }
                        ViewState["ListtblCONTACTBus"] = null;
                    }
                    //   btnSubmit.Text = "Add";

                }
                else
                {

                    int SeqCount = DB.TBLCOMPANYSETUPs.Where(p => p.TenentID == TID).Count();
                    Database.TBLCOMPANYSETUP objtbl_COMPANYSETUP = new Database.TBLCOMPANYSETUP();
                    objtbl_COMPANYSETUP.TenentID = TID;
                    objtbl_COMPANYSETUP.COMPID = DB.TBLCOMPANYSETUPs.Where(p => p.TenentID == TID).Count() > 0 ? Convert.ToInt32(DB.TBLCOMPANYSETUPs.Where(p => p.TenentID == TID).Max(p => p.COMPID) + 1) : 1;
                    ViewState["CIDRF"] = Convert.ToInt32(objtbl_COMPANYSETUP.COMPID);
                    objtbl_COMPANYSETUP.PHYSICALLOCID = txtCustomerName.Text;
                    objtbl_COMPANYSETUP.COMPNAME1 = txtCustomerName.Text;
                    objtbl_COMPANYSETUP.COMPNAME2 = txtCustomer.Text;
                    objtbl_COMPANYSETUP.COMPNAME3 = txtCustomer2.Text;
                    objtbl_COMPANYSETUP.EMAIL1 = tags_2.Text;
                    // objtbl_COMPANYSETUP.EMAIL2 = tags_2.Text;
                    objtbl_COMPANYSETUP.ITMANAGER = drpItManager.SelectedValue;
                    objtbl_COMPANYSETUP.ADDR1 = txtAddress.Text;
                    objtbl_COMPANYSETUP.ADDR2 = txtAddress2.Text;
                    objtbl_COMPANYSETUP.CITY = txtCity.Text;
                    objtbl_COMPANYSETUP.STATE = drpSates.SelectedValue;
                    objtbl_COMPANYSETUP.POSTALCODE = txtPostalCode.Text;
                    objtbl_COMPANYSETUP.ZIPCODE = txtZipCode.Text;
                    objtbl_COMPANYSETUP.MYCONLOCID = Convert.ToInt32(drpMyCounLocID.SelectedValue);
                    objtbl_COMPANYSETUP.MYPRODID = Convert.ToInt32(drpMyProductId.SelectedValue);
                    //  string CNAME = drpCountry.SelectedItem.ToString();
                    objtbl_COMPANYSETUP.COUNTRYID = Convert.ToInt32(drpCountry.SelectedValue);
                    objtbl_COMPANYSETUP.BUSPHONE1 = tags_4.Text;
                    //objtbl_COMPANYSETUP.BUSPHONE2 = tags_4.Text;
                    //objtbl_COMPANYSETUP.BUSPHONE3 = tags_4.Text;
                    //objtbl_COMPANYSETUP.BUSPHONE4 = tags_4.Text;
                    objtbl_COMPANYSETUP.MOBPHONE = txtMobileNo.Text;
                    objtbl_COMPANYSETUP.FAX = tags_3.Text;
                    //  objtbl_COMPANYSETUP.FAX2 = tags_3.Text;
                    objtbl_COMPANYSETUP.PRIMLANGUGE = drpPrimaryLang.SelectedValue;
                    objtbl_COMPANYSETUP.WEBPAGE = txtWebsite.Text;
                    objtbl_COMPANYSETUP.CompanyType = drpType.SelectedValue;
                    objtbl_COMPANYSETUP.ISMINISTRY = chbIsMinistry.Checked ? true : false;
                    objtbl_COMPANYSETUP.ISSMB = chbIssMb.Checked ? true : false;
                    objtbl_COMPANYSETUP.ISCORPORATE = chbIsCorporate.Checked ? true : false;
                    objtbl_COMPANYSETUP.INHAWALLY = chbInHawally.Checked ? true : false;
                    objtbl_COMPANYSETUP.SALER = chbSaler.Checked ? true : false;
                    objtbl_COMPANYSETUP.BUYER = chbBuyer.Checked ? true : false;
                    objtbl_COMPANYSETUP.SALEDEPROD = chbSaleDeProd.Checked ? true : false;
                    objtbl_COMPANYSETUP.EMAISUB = chbEmailSub.Checked ? true : false;
                    objtbl_COMPANYSETUP.EMAILSUBDATE = DateTime.Now;
                    objtbl_COMPANYSETUP.PRODUCTDEALIN = tags_5.Text;
                    objtbl_COMPANYSETUP.REMARKS = txtRemark.Text;
                    objtbl_COMPANYSETUP.Keyword = tags_1.Text;
                    objtbl_COMPANYSETUP.CUSERID = txtcUserName.Text;
                    objtbl_COMPANYSETUP.CPASSWRD = txtcPassword.Text;
                    objtbl_COMPANYSETUP.USERID = UID;
                    objtbl_COMPANYSETUP.ENTRYDATE = DateTime.Now;
                    objtbl_COMPANYSETUP.ENTRYTIME = DateTime.Now;
                    objtbl_COMPANYSETUP.UPDTTIME = DateTime.Now;
                    objtbl_COMPANYSETUP.Approved = 0;
                    objtbl_COMPANYSETUP.Active = "Y";
                    DB.TBLCOMPANYSETUPs.AddObject(objtbl_COMPANYSETUP);
                    DB.SaveChanges();
                    //objtbl_COMPANYSETUP.COMPNAME = txtCustomer.Text;    //other language baki
                    // objtbl_COMPANYSETUP.Keyword = tags_1.Text;
                    if (tags_1.Text != "")
                    {
                        string[] Seperate4 = tags_1.Text.Split(',');
                        int count4 = 0;
                        string Sep4 = "";
                        for (int i = 0; i <= Seperate4.Count() - 1; i++)
                        {
                            Sep4 = Seperate4[i];

                            count4++;
                            Tbl_RecordType_Mst obj = new Tbl_RecordType_Mst();
                            obj.TenentID = TID;
                            obj.RecordType = "Classification";
                            obj.RecTypeID = DB.Tbl_RecordType_Mst.Where(p => p.TenentID == TID).Count() > 0 ? Convert.ToInt32(DB.Tbl_RecordType_Mst.Where(p => p.TenentID == TID).Max(p => p.RecTypeID) + 1) : 1;
                            obj.CompniyAndContactID = objtbl_COMPANYSETUP.COMPID;
                            obj.RunSerial = count4;
                            obj.Recource = 5005;
                            obj.RecourceName = "Company";
                            obj.RecValue = Seperate4[i];
                            obj.Active = true;
                            // obj.Rremark = "AutomatedProcess";
                            DB.Tbl_RecordType_Mst.AddObject(obj);
                            DB.SaveChanges();
                        }
                    }

                    if (tags_5.Text != "")
                    {
                        string[] Seperate5 = tags_5.Text.Split(',');
                        int count5 = 0;
                        string Sep5 = "";
                        for (int i = 0; i <= Seperate5.Count() - 1; i++)
                        {
                            Sep5 = Seperate5[i];

                            count5++;
                            Tbl_RecordType_Mst obj = new Tbl_RecordType_Mst();
                            obj.TenentID = TID;
                            obj.RecordType = "BRAND";
                            obj.RecTypeID = DB.Tbl_RecordType_Mst.Where(p => p.TenentID == TID).Count() > 0 ? Convert.ToInt32(DB.Tbl_RecordType_Mst.Where(p => p.TenentID == TID).Max(p => p.RecTypeID) + 1) : 1;
                            obj.CompniyAndContactID = objtbl_COMPANYSETUP.COMPID;
                            obj.RunSerial = count5;
                            obj.Recource = 5005;
                            obj.RecourceName = "Company";
                            obj.RecValue = Seperate5[i];
                            obj.Active = true;
                            // obj.Rremark = "AutomatedProcess";
                            DB.Tbl_RecordType_Mst.AddObject(obj);
                            DB.SaveChanges();
                        }
                    }


                    if (tags_2.Text != "")
                    {
                        string[] Seperate = tags_2.Text.Split(',');
                        int count = 0;
                        string Sep = "";
                        for (int i = 0; i <= Seperate.Count() - 1; i++)
                        {
                            Sep = Seperate[i];
                            var exist = DB.Tbl_RecordType_Mst.Where(c => c.RecValue == Sep && c.RecourceName == "Company" && c.RecordType == "Email" && c.TenentID == TID);
                            if (exist.Count() <= 0)
                            {
                                count++;
                                Tbl_RecordType_Mst obj = new Tbl_RecordType_Mst();
                                obj.TenentID = TID;
                                obj.RecordType = "Email";
                                obj.RecTypeID = DB.Tbl_RecordType_Mst.Where(p => p.TenentID == TID).Count() > 0 ? Convert.ToInt32(DB.Tbl_RecordType_Mst.Where(p => p.TenentID == TID).Max(p => p.RecTypeID) + 1) : 1;
                                obj.CompniyAndContactID = objtbl_COMPANYSETUP.COMPID;
                                obj.RunSerial = count;
                                obj.Recource = 5005;
                                obj.RecourceName = "Company";
                                obj.RecValue = Seperate[i];
                                obj.Active = true;
                                // obj.Rremark = "AutomatedProcess";
                                DB.Tbl_RecordType_Mst.AddObject(obj);
                                DB.SaveChanges();
                            }

                            else
                            {
                                string display = "Email Is Duplicate!";
                                ClientScript.RegisterStartupScript(this.GetType(), "Email Is Duplicate!", "alert('" + display + "');", true);
                                return;
                            }
                        }
                    }
                    if (tags_3.Text != "")
                    {
                        string[] Seperate1 = tags_3.Text.Split(',');
                        int count1 = 0;
                        string Sep1 = "";
                        for (int i = 0; i <= Seperate1.Count() - 1; i++)
                        {
                            Sep1 = Seperate1[i];
                            var exist = DB.Tbl_RecordType_Mst.Where(c => c.RecValue == Sep1 && c.RecourceName == "Company" && c.RecordType == "Fax" && c.TenentID == TID);
                            if (exist.Count() <= 0)
                            {
                                count1++;
                                Tbl_RecordType_Mst obj = new Tbl_RecordType_Mst();
                                obj.TenentID = TID;
                                obj.RecordType = "Fax";
                                obj.RecTypeID = DB.Tbl_RecordType_Mst.Where(p => p.TenentID == TID).Count() > 0 ? Convert.ToInt32(DB.Tbl_RecordType_Mst.Where(p => p.TenentID == TID).Max(p => p.RecTypeID) + 1) : 1; ;
                                obj.CompniyAndContactID = objtbl_COMPANYSETUP.COMPID;
                                obj.RunSerial = count1;
                                obj.Recource = 5005;
                                obj.RecourceName = "Company";
                                obj.RecValue = Seperate1[i];
                                obj.Active = true;
                                // obj.Rremark = "AutomatedProcess";
                                DB.Tbl_RecordType_Mst.AddObject(obj);
                                DB.SaveChanges();

                            }
                            else
                            {
                                string display = "Fax Number Is Duplicate!";
                                ClientScript.RegisterStartupScript(this.GetType(), "Fax Number Is Duplicate!", "alert('" + display + "');", true);
                                return;
                            }
                        }
                    }
                    if (tags_4.Text != "")
                    {
                        string[] Seperate2 = tags_4.Text.Split('/');
                        int count2 = 0;
                        string Sep2 = "";
                        for (int i = 0; i <= Seperate2.Count() - 1; i++)
                        {
                            Sep2 = Seperate2[i];
                            var exist = DB.Tbl_RecordType_Mst.Where(c => c.RecValue == Sep2 && c.RecourceName == "Company" && c.RecordType == "BusPhone" && c.TenentID == TID);
                            if (exist.Count() <= 0)
                            {
                                count2++;
                                Tbl_RecordType_Mst obj = new Tbl_RecordType_Mst();
                                obj.TenentID = TID;
                                obj.RecordType = "BusPhone";
                                obj.RecTypeID = DB.Tbl_RecordType_Mst.Where(p => p.TenentID == TID).Count() > 0 ? Convert.ToInt32(DB.Tbl_RecordType_Mst.Where(p => p.TenentID == TID).Max(p => p.RecTypeID) + 1) : 1; ;
                                obj.CompniyAndContactID = objtbl_COMPANYSETUP.COMPID;
                                obj.RunSerial = count2;
                                obj.Recource = 5005;
                                obj.RecourceName = "Company";
                                obj.RecValue = Seperate2[i];
                                obj.Active = true;
                                // obj.Rremark = "AutomatedProcess";
                                DB.Tbl_RecordType_Mst.AddObject(obj);
                                DB.SaveChanges();

                            }
                            else
                            {
                                string display = "Bus Number Is Duplicate!";
                                ClientScript.RegisterStartupScript(this.GetType(), "Bus Number Is Duplicate!", "alert('" + display + "');", true);
                                return;
                            }
                        }
                    }

                    if (ViewState["ListtblCONTACTBus"] != null)
                    {
                        var Listbus = ((List<Database.tblCONTACTBu>)ViewState["ListtblCONTACTBus"]).ToList();
                        for (int i = 0; i < Listbus.Count(); i++)
                        {
                            Database.tblCONTACTBu objtbl_CONTACTBus = new Database.tblCONTACTBu();
                            objtbl_CONTACTBus.TenentID = Listbus[i].TenentID;
                            objtbl_CONTACTBus.BusPhone1 = Listbus[i].BusPhone1;
                            objtbl_CONTACTBus.ContactMyID = Listbus[i].ContactMyID;
                            objtbl_CONTACTBus.CompID = Listbus[i].CompID;
                            objtbl_CONTACTBus.Country = Listbus[i].Country;
                            objtbl_CONTACTBus.email2 = Listbus[i].email2;
                            objtbl_CONTACTBus.Fax = Listbus[i].Fax;
                            objtbl_CONTACTBus.JobTitle = Listbus[i].JobTitle;
                            objtbl_CONTACTBus.PhysicalLocID = Listbus[i].PhysicalLocID;
                            objtbl_CONTACTBus.remarks = Listbus[i].remarks;
                            objtbl_CONTACTBus.Active = Listbus[i].Active;
                            DB.tblCONTACTBus.AddObject(objtbl_CONTACTBus);
                            DB.SaveChanges();

                        }
                        ViewState["ListtblCONTACTBus"] = null;
                    }

                }

                BindData();
                // clen();
                redonlyfalse();
                btnSubmit.Visible = false;
                Button1.Visible = true;
                btnattmentmst.Visible = false;
                scope.Complete(); //  To commit.
                //}
                //catch (Exception ex)
                //{
                //    throw;
                //}
            }


        }
        public void BindDataClassfic()
        {
            Classes.CRMClass.getcrmdropdown(drpCategory, TID, "7861", "1", "", "CAT_MST");
            //select * from CAT_MST

            //drpCategory.DataSource = DB.CAT_MST.Where(p => p.PARENT_CATID == 7861 && p.Active == "1").OrderBy(a => a.CAT_NAME1);
            //drpCategory.DataTextField = "cat_name1";
            //drpCategory.DataValueField = "cat_name1";
            //drpCategory.DataBind();
            //drpCategory.Items.Insert(0, new ListItem("-- Select --", "0"));
        }
        public void FillContractorID()
        {
            BindDataClassfic();

            Classes.EcommAdminClass.getdropdown(drpType, TID, "COMP", "COMPTYPE", "", "REFTABLE");
            //select * from REFTABLE where REFTYPE = 'COMP' and REFSUBTYPE = 'COMPTYPE'

            //drpType.DataSource = DB.REFTABLEs.Where(P => P.REFTYPE == "COMP" && P.REFSUBTYPE == "COMPTYPE");
            //drpType.DataTextField = "REFNAME1";
            //drpType.DataValueField = "REFID";
            //drpType.DataBind();

            //string classification = DB.CAT_MST.Single(p => p.catid == 7861 && p.parent_catid == 0).cat_name1;

            //var list = from item in DB.CAT_MST.Where(p => p.parent_catid == 7861) select new { item.catid, cat_name11 = " " + classification + "/" + item.cat_name1 };

            Classes.EcommAdminClass.getdropdown(drpCountry, TID, "", "", "", "tblCOUNTRY");


            //drpCountry.SelectedValue = "126";
            //string CID = drpCountry.SelectedValue;
            //bindSates(CID);



            //select * from tblCOUNTRY where Active= 'Y' order by COUNAME1

            //drpCountry.DataSource = DB.tblCOUNTRies.Where(p => p.Active == "Y").OrderBy(p=>p.COUNAME1);
            //drpCountry.DataTextField = "COUNAME1";
            //drpCountry.DataValueField = "COUNTRYID";
            //drpCountry.DataBind();
            //drpCountry.Items.Insert(0, new ListItem("-- Select --", "0"));
            drpSates.Items.Insert(0, new ListItem("-- Select State--", "0"));
            //l1.Attributes.CssStyle

            Classes.EcommAdminClass.getdropdown(drpItManager, TID, "", "", "", "Tbl_Position_Mst");
            //select * from Tbl_Position_Mst where Active= 'true' 

            //drpItManager.DataSource = DB.Tbl_Position_Mst.Where(P => P.TenentID == TID && P.Active == true);
            //drpItManager.DataTextField = "PositionName";
            //drpItManager.DataValueField = "PositionID";
            //drpItManager.DataBind();
            //drpItManager.Items.Insert(0, new ListItem("-- Select --", "0"));

            Classes.EcommAdminClass.getdropdown(drpMyCounLocID, TID, "", "", "", "TBLCOMPANY_LOCATION");
            //select * from TBLCOMPANY_LOCATION where Active= 'Y'

            //drpMyCounLocID.DataSource = DB.TBLCOMPANY_LOCATION.Where(P => P.TenentID == TID && P.ACTIVE == "Y");
            //drpMyCounLocID.DataTextField = "LOCATION_NAME1";
            //drpMyCounLocID.DataValueField = "LOCATIONID";
            //drpMyCounLocID.DataBind();
            //drpMyCounLocID.Items.Insert(0, new ListItem("-- Select --", "0"));

            Classes.EcommAdminClass.getdropdown(drpPrimaryLang, TID, "", "", "", "tblLanguage");
            //select * from tblLanguage where ACTIVE= 'Y'

            //drpPrimaryLang.DataSource = DB.tblLanguages.Where(P => P.TenentID == TID && P.ACTIVE == "Y").OrderBy(a => a.LangName1);
            //drpPrimaryLang.DataTextField = "LangName1";
            //drpPrimaryLang.DataValueField = "MYCONLOCID";
            //drpPrimaryLang.DataBind();
            //drpPrimaryLang.Items.Insert(0, new ListItem("-- Select --", "0"));

            Classes.EcommAdminClass.getdropdown(drpMyProductId, TID, "", "", "", "TBLPRODUCT");
            //select * from TBLPRODUCT where ACTIVE= 1 and TenentID = 1

            //drpMyProductId.DataSource = DB.TBLPRODUCTs.Where(P => P.TenentID == TID && P.ACTIVE == "Y").OrderBy(a => a.ProdName1);
            //drpMyProductId.DataTextField = "ProdName1";
            //drpMyProductId.DataValueField = "MYPRODID";
            //drpMyProductId.DataBind();
            //drpMyProductId.Items.Insert(0, new ListItem("-- Select --", "0"));

            //drpCompnay.DataSource = DB.TBLCONTACT.Where(P => P.TenentID == TID && P.Active == "Y" && P.PHYSICALLOCID != "HLY").OrderBy(a => a.PersName1);
            //drpCompnay.DataTextField = "PersName1";
            //drpCompnay.DataValueField = "ContactMyID";
            //drpCompnay.DataBind();
            drpCompnay.Items.Insert(0, new ListItem("-- Contact Search --", "0"));

            Classes.EcommAdminClass.getdropdown(drpSomib, TID, "social", "media", "", "REFTABLE");
            //select * from REFTABLE where REFTYPE = 'social'  and REFSUBTYPE= 'media' 

            //drpSomib.DataSource = DB.REFTABLEs.Where(P => P.REFTYPE == "social" && P.REFSUBTYPE == "media" && P.ACTIVE == "Y").OrderBy(a => a.REFNAME1);
            //drpSomib.DataTextField = "REFNAME1";
            //drpSomib.DataValueField = "REFID";
            //drpSomib.DataBind();
            //drpSomib.Items.Insert(0, new ListItem("-- Select --", "0"));

            Classes.EcommAdminClass.getdropdown(drpbrand, TID, "BRAND", "OTH", "", "REFTABLE");
            //select * from REFTABLE where REFTYPE = 'BRAND'  and REFSUBTYPE= 'OTH' 

            //drpbrand.DataSource = DB.REFTABLEs.Where(P => P.REFTYPE == "BRAND" && P.REFSUBTYPE == "OTH" && P.ACTIVE == "Y").OrderBy(a => a.REFNAME1);
            //drpbrand.DataTextField = "REFNAME1";
            //drpbrand.DataValueField = "REFID";
            //drpbrand.DataBind();
            //drpbrand.Items.Insert(0, new ListItem("-- Select --", "0"));

            //drpSort.Items.Clear();
            //drpSort.Items.Insert(0, new ListItem("---Sorting By---", "0"));
            //drpSort.Items.Insert(1, new ListItem(lblcustemername.Text, "1"));
            ////drpSort.Items.Insert(2, new ListItem(lbladdresh.Text, "2"));
            ////drpSort.Items.Insert(2, new ListItem(lblemaillist.Text, "3"));
            ////drpSort.Items.Insert(3, new ListItem(lblmobiellis.Text, "4"));
            ////drpSort.Items.Insert(4, new ListItem(lblstatelis.Text, "5"));
            ////drpSort.Items.Insert(5, new ListItem(lblzipcodelit.Text, "6"));
            //drpSort.Items.Insert(2, new ListItem(lblcitylit.Text, "7"));
           // drpSort.Items.Insert(7, new ListItem(lblremarklis.Text, "8"));
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            redonlyfalse();
            btnSubmit.Visible = false;
            Button1.Visible = true;
            // btnSubmit.Text = "New";
            Lastdataredmode();
            btnattmentmst.Visible = false;
            // Response.Redirect("CompanyMaster.aspx");
        }

        public string getStste(int SID)
        {
            return DB.tblStates.SingleOrDefault(p => p.StateID == SID ).MYNAME1;
        }

        public string getshocial(int SMID)
        {
            return DB.REFTABLEs.SingleOrDefault(p => p.REFID == SMID && p.TenentID == TID).REFNAME1;
        }

        protected void btnCompanyCl_Click(object sender, EventArgs e)
        {




        }

        protected void btnCustomerN1_Click(object sender, EventArgs e)
        {
            lblCustomerName.Text = "";
            var exist = DB.TBLCOMPANYSETUPs.Where(c => c.COMPNAME1 == txtCustomerName.Text && c.TenentID == TID);
            if (exist.Count() <= 0)
            {

            }
            else
            {
                // ModalPopupExtender4.TargetControlID = btntest4.ID;
                ModalPopupExtender4.Show();
            }
        }

        protected void btnCompanyN2_Click(object sender, EventArgs e)
        {
            lblCustomerL1.Text = "";
            var exist = DB.TBLCOMPANYSETUPs.Where(c => c.COMPNAME2 == txtCustomer.Text && c.TenentID == TID).ToList();
            if (exist.Count() <= 0)
            {

            }
            else
            {
                lblCustomerL1.Text = "Company Name 2 Is Duplicate";
            }
        }

        protected void btncompnyN3_Click(object sender, EventArgs e)
        {
            lblCustomerL2.Text = "";
            var exist = DB.TBLCOMPANYSETUPs.Where(c => c.COMPNAME3 == txtCustomer2.Text && c.TenentID == TID);
            if (exist.Count() <= 0)
            {

            }
            else
            {
                lblCustomerL2.Text = "Company Name 3 Is Duplicate";
            }
        }
        protected void btnEmail_Click(object sender, EventArgs e)
        {
            lblEmail12.Text = "";
            string[] Seperate = tags_2.Text.Split('/');

            string Sep = "";

            for (int i = 0; i <= Seperate.Count() - 1; i++)
            {

                Sep = Seperate[i];

                if (Sep.Contains(".") || Sep.Contains("@"))
                {

                    var exist = DB.Tbl_RecordType_Mst.Where(c => c.RecValue == Sep && c.TenentID == TID);
                    if (exist.Count() <= 0)
                    {
                    }
                    else
                    {
                        int compname = DB.Tbl_RecordType_Mst.Single(c => c.RecValue == Sep && c.TenentID == TID).CompniyAndContactID;
                        lblEmail12.Text = "Email Is Duplicate and It's allready used for this " + DB.TBLCOMPANYSETUPs.Single(p => p.COMPID == compname && p.TenentID == TID).COMPNAME1;
                    }
                }
                else
                {
                    lblEmail12.Text = "Invalid Email: " + Sep;
                }
            }
        }
        protected void btnFax_Click(object sender, EventArgs e)
        {
            Label21.Text = "";

            string CNAME = drpCountry.SelectedItem.Text;
            int CID = Convert.ToInt32(drpCountry.SelectedValue);
            int Flenth = Convert.ToInt32(DB.tblCOUNTRies.Single(p => p.COUNTRYID == CID && p.TenentID == TID).FaxLength);

            string[] Seperate1 = tags_3.Text.Split('/');

            string Sep1 = "";
            for (int i = 0; i <= Seperate1.Count() - 1; i++)
            {
                Sep1 = Seperate1[i];
                if (Sep1.Length == Flenth)
                {

                    var exist = DB.Tbl_RecordType_Mst.Where(c => c.RecValue == Sep1 && c.TenentID == TID);
                    if (exist.Count() <= 0)
                    {



                    }
                    else
                    {
                        int compname = DB.Tbl_RecordType_Mst.Single(c => c.RecValue == Sep1 && c.TenentID == TID).CompniyAndContactID;
                        Label21.Text = "Fax Number Is Duplicate and It's allready used for this  " + DB.TBLCOMPANYSETUPs.Single(p => p.COMPID == compname && p.TenentID == TID).COMPNAME1;
                    }
                }
                else
                {
                    Label21.Text = CNAME + "Country is Maximun And Minimum " + Flenth + " Digit Requried " + Sep1;
                }
            }

        }

        protected void btnBusPhone_Click(object sender, EventArgs e)
        {
            Label22.Text = "";
            string CNAME = drpCountry.SelectedItem.Text;
            int CID = Convert.ToInt32(drpCountry.SelectedValue);
            int Flenth = Convert.ToInt32(DB.tblCOUNTRies.Single(p => p.COUNTRYID == CID && p.TenentID == TID).TelLength);
            string[] Seperate2 = tags_4.Text.Split('/');

            string Sep2 = "";
            for (int i = 0; i <= Seperate2.Count() - 1; i++)
            {
                Sep2 = Seperate2[i];
                if (Sep2.Length == Flenth)
                {

                    var exist = DB.Tbl_RecordType_Mst.Where(c => c.RecValue == Sep2 && c.TenentID == TID);
                    if (exist.Count() <= 0)
                    {



                    }
                    else
                    {
                        int compname = DB.Tbl_RecordType_Mst.Single(c => c.RecValue == Sep2 && c.TenentID == TID).CompniyAndContactID;
                        Label22.Text = "Bus Phone Is Duplicate and It's allready used for this " + DB.TBLCOMPANYSETUPs.Single(p => p.COMPID == compname && p.TenentID == TID).COMPNAME1;
                    }
                }
                else
                {
                    Label22.Text = CNAME + "Country is Maximun And Minimum " + Flenth + " Digit Requried " + Sep2;
                }
            }
        }
        protected void btnMobile_Click(object sender, EventArgs e)
        {
            lblMobileNo.Text = "";
            string CNAME = drpCountry.SelectedItem.Text;
            int CID = Convert.ToInt32(drpCountry.SelectedValue);
            int Flenth = Convert.ToInt32(DB.tblCOUNTRies.Single(p => p.COUNTRYID == CID && p.TenentID == TID).TelLength);

            if (txtMobileNo.Text.Length == Flenth)
            {
                var exist = DB.TBLCOMPANYSETUPs.Where(c => c.MOBPHONE == txtMobileNo.Text && c.TenentID == TID);
                if (exist.Count() <= 0)
                {

                }
                else
                {
                    lblMobileNo.Text = "Mobile Number Is Duplicate";
                }
            }
            else
            {
                lblMobileNo.Text = CNAME + "Country is Maximun And Minimum " + Flenth + " Digit Requried " + txtMobileNo.Text;
            }

        }

        protected void btnUrl_Click(object sender, EventArgs e)
        {
            lblWebsite.Text = "";
            var exist = DB.TBLCOMPANYSETUPs.Where(c => c.WEBPAGE == txtWebsite.Text && c.TenentID == TID);
            if (exist.Count() <= 0)
            {

            }
            else
            {
                var List = DB.TBLCOMPANYSETUPs.Where(p => p.WEBPAGE == txtWebsite.Text && p.TenentID == TID).ToList();
                string ComNamr = List[0].COMPNAME1;
                lblWebsite.Text = "WebSite Is Duplicate and It's allready used for this " + ComNamr;
            }

        }

        protected void btnUserName_Click(object sender, EventArgs e)
        {
            lblcUserName.Text = "";
            var exist = DB.TBLCOMPANYSETUPs.Where(c => c.CUSERID == txtcUserName.Text && c.TenentID == TID);
            if (exist.Count() <= 0)
            {

            }
            else
            {
                lblcUserName.Text = "UserName Is Duplicate";
            }

        }
        public string getStste1(int SID)
        {
            return DB.tblStates.SingleOrDefault(p => p.StateID == SID).MYNAME1;
        }

        public string getcompniy(int CID)
        {
            if (DB.TBLCOMPANYSETUPs.Where(p => p.COMPID == CID && p.TenentID == TID).Count() > 0)
                return DB.TBLCOMPANYSETUPs.SingleOrDefault(p => p.COMPID == CID && p.TenentID == TID).COMPNAME1;
            else
                return txtCustomerName.Text;
        }

        public string getcity(int GCID)
        {
            if (DB.TBLCOMPANYSETUPs.Where(p => p.COMPID == GCID && p.TenentID == TID).Count() > 0)
                return DB.TBLCOMPANYSETUPs.SingleOrDefault(p => p.COMPID == GCID && p.TenentID == TID).CITY;
            else
                return txtCity.Text;
        }
        public string getstate(int GCID)
        {
            if (DB.TBLCOMPANYSETUPs.Where(p => p.COMPID == GCID && p.TenentID == TID).Count() > 0)
                return DB.TBLCOMPANYSETUPs.SingleOrDefault(p => p.COMPID == GCID && p.TenentID == TID).STATE;
            else
                return drpSates.SelectedValue;
        }
        protected void txtCustomerName_TextChanged(object sender, EventArgs e)
        {
            lblCustomerName.Text = "";
            if (!string.IsNullOrEmpty(txtCustomerName.Text))
            {
                var exist = DB.TBLCOMPANYSETUPs.Where(c => c.COMPNAME1 == txtCustomerName.Text && c.TenentID == TID);
                var UserList = DB.TBLCOMPANYSETUPs.SingleOrDefault(p => p.COMPNAME1 == txtCustomerName.Text && p.TenentID == TID);

                if (exist.Count() <= 0)
                {

                    lblCustomerName.Text = "Customer Name  Available";
                    txtCustomer.Text = Translate(txtCustomerName.Text, "ar");
                    txtCustomer2.Text = Translate(txtCustomerName.Text, "fr");
                    updCompny.Update();
                }
                else
                {
                    ViewState["compId"] = UserList.COMPID;
                    //ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "myscript", "alert('Customer Name Already Taken.');", true);
                    //lblCustomerName.Text = "Customer Name Already Taken"; 
                    //  ModalPopupExtender4.TargetControlID = btntest4.ID;
                    ModalPopupExtender4.Show();
                    //BindData1();
                    labelCopop.Text = UserList.COMPNAME1;
                    lblmopop.Text = UserList.MOBPHONE;
                    lblEmailpop.Text = UserList.EMAIL1;
                    lblFaxpop.Text = UserList.FAX;
                    lblBuspop.Text = UserList.BUSPHONE1;
                    lblusernamepop.Text = UserList.CUSERID;
                }
            }
            else
            {
                lblCustomerName.Text = "Insert The Customer Name Available";
            }
        }

        protected void btnYes_Click(object sender, EventArgs e)
        {
            int CID = Convert.ToInt32(ViewState["compId"]);
            //  Database.TBLCOMPANYSETUP objtbl_COMPANYSETUP = DB.TBLCOMPANYSETUP.Single(p => p.TenentID == TID && p.COMPID == CID);

            BindEditcompniy(CID);
            updCompny.Update();
            redonlyture();
            lblBusContactDe.Text = "Business Company Details -Edit Mode";
            lblBCDeta.Text = "Business Company Details -Edit Mode";
            lblwExistance.Text = "Web Existance -Edit Mode";
            lblWEmp.Text = "Working Employees -Edit Mode";
            btnSubmit.Text = "Update";
        }

        protected void btnNo_Click(object sender, EventArgs e)
        {

        }

        protected void txtCustomer_TextChanged(object sender, EventArgs e)
        {
            lblCustomerL1.Text = "";
            if (!string.IsNullOrEmpty(txtCustomer.Text))
            {
                var exist = DB.TBLCOMPANYSETUPs.Where(c => c.COMPNAME2 == txtCustomer.Text && c.TenentID == TID);
                var UserList = DB.TBLCOMPANYSETUPs.SingleOrDefault(p => p.COMPNAME2 == txtCustomer.Text && p.TenentID == TID);

                if (exist.Count() <= 0)
                {

                    lblCustomerL1.Text = "Customer Name  Available";
                }
                else
                {
                    ViewState["compId"] = UserList.COMPID;
                    //ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "myscript", "alert('Customer Name Already Taken.');", true);
                    //lblCustomerName.Text = "Customer Name Already Taken"; 
                    //  ModalPopupExtender4.TargetControlID = btntest4.ID;
                    ModalPopupExtender4.Show();
                    //BindData1();
                    labelCopop.Text = UserList.COMPNAME1;
                    lblmopop.Text = UserList.MOBPHONE;
                    lblEmailpop.Text = UserList.EMAIL1;
                    lblFaxpop.Text = UserList.FAX;
                    lblBuspop.Text = UserList.BUSPHONE1;
                    lblusernamepop.Text = UserList.CUSERID;
                }
            }
            else
            {
                lblCustomerL1.Text = "Insert The Customer Name Available";
            }
        }

        protected void txtCustomer2_TextChanged(object sender, EventArgs e)
        {
            lblCustomerL2.Text = "";
            if (!string.IsNullOrEmpty(txtCustomer2.Text))
            {
                var exist = DB.TBLCOMPANYSETUPs.Where(c => c.COMPNAME3 == txtCustomer2.Text && c.TenentID == TID);
                var UserList = DB.TBLCOMPANYSETUPs.SingleOrDefault(p => p.COMPNAME3 == txtCustomer2.Text && p.TenentID == TID);

                if (exist.Count() <= 0)
                {

                    lblCustomerL2.Text = "Customer Name  Available";
                }
                else
                {
                    ViewState["compId"] = UserList.COMPID;
                    //ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "myscript", "alert('Customer Name Already Taken.');", true);
                    //lblCustomerName.Text = "Customer Name Already Taken"; 
                    //  ModalPopupExtender4.TargetControlID = btntest4.ID;
                    ModalPopupExtender4.Show();
                    //BindData1();
                    labelCopop.Text = UserList.COMPNAME1;
                    lblmopop.Text = UserList.MOBPHONE;
                    lblEmailpop.Text = UserList.EMAIL1;
                    lblFaxpop.Text = UserList.FAX;
                    lblBuspop.Text = UserList.BUSPHONE1;
                    lblusernamepop.Text = UserList.CUSERID;
                }
            }
            else
            {
                lblCustomerL2.Text = "Insert The Customer Name Available";
            }
        }

        protected void txtMobileNo_TextChanged(object sender, EventArgs e)
        {
            lblMobileNo.Text = "";
            if (!string.IsNullOrEmpty(txtMobileNo.Text))
            {
                var exist = DB.TBLCOMPANYSETUPs.Where(c => c.MOBPHONE == txtMobileNo.Text && c.TenentID == TID);
                var UserList = DB.TBLCOMPANYSETUPs.SingleOrDefault(p => p.MOBPHONE == txtMobileNo.Text && p.TenentID == TID);

                if (exist.Count() <= 0)
                {

                    lblMobileNo.Text = "Mobiel Number is Available";
                }
                else
                {
                    ViewState["compId"] = UserList.COMPID;
                    //ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "myscript", "alert('Customer Name Already Taken.');", true);
                    //lblCustomerName.Text = "Customer Name Already Taken"; 
                    //  ModalPopupExtender4.TargetControlID = btntest4.ID;
                    ModalPopupExtender4.Show();
                    //BindData1();
                    labelCopop.Text = UserList.COMPNAME1;
                    lblmopop.Text = UserList.MOBPHONE;
                    lblEmailpop.Text = UserList.EMAIL1;
                    lblFaxpop.Text = UserList.FAX;
                    lblBuspop.Text = UserList.BUSPHONE1;
                    lblusernamepop.Text = UserList.CUSERID;
                }
            }
            else
            {
                lblMobileNo.Text = "Insert The Mobiel Number !";
            }
        }

        protected void txtcUserName_TextChanged(object sender, EventArgs e)
        {
            lblcUserName.Text = "";
            if (!string.IsNullOrEmpty(txtcUserName.Text))
            {
                var exist = DB.TBLCOMPANYSETUPs.Where(c => c.CUSERID == txtcUserName.Text && c.TenentID == TID);
                var UserList = DB.TBLCOMPANYSETUPs.SingleOrDefault(p => p.CUSERID == txtcUserName.Text && p.TenentID == TID);

                if (exist.Count() <= 0)
                {

                    lblcUserName.Text = "User Name is Available";
                }
                else
                {
                    ViewState["compId"] = UserList.COMPID;
                    //ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "myscript", "alert('Customer Name Already Taken.');", true);
                    //lblCustomerName.Text = "Customer Name Already Taken"; 
                    //  ModalPopupExtender4.TargetControlID = btntest4.ID;
                    ModalPopupExtender4.Show();
                    //BindData1();
                    labelCopop.Text = UserList.COMPNAME1;
                    lblmopop.Text = UserList.MOBPHONE;
                    lblEmailpop.Text = UserList.EMAIL1;
                    lblFaxpop.Text = UserList.FAX;
                    lblBuspop.Text = UserList.BUSPHONE1;
                    lblusernamepop.Text = UserList.CUSERID;
                }
            }
            else
            {
                lblcUserName.Text = "Insert The User Name !";
            }
        }

        protected void btnSocial_Click(object sender, EventArgs e)
        {
            int CID123 = Convert.ToInt32(ViewState["compId"]);
            int COMPID123 = 0;
            if (Request.QueryString["COMPID"] != null || CID123 != 0)
            {
                int COMPID13 = Convert.ToInt32(Request.QueryString["COMPID"]);
                if (COMPID13 != null && COMPID13 != 0)
                    COMPID123 = COMPID13;
                else
                    COMPID123 = CID123;
            }
            else
            {
                COMPID123 = DB.TBLCOMPANYSETUPs.Where(p => p.TenentID == TID).Count() > 0 ? Convert.ToInt32(DB.TBLCOMPANYSETUPs.Where(p => p.TenentID == TID).Max(p => p.COMPID) + 1) : 1;
            }

            int COMPID = Convert.ToInt32(ViewState["CIDRF"]);
          
            int SID = Convert.ToInt32(drpSomib.SelectedValue);
            var exist = DB.Tbl_RecordType_Mst.Where(c => c.RecValue == txtSocial.Text && c.Recource == SID && c.TenentID == TID);
            if (exist.Count() <= 0)
            {

                Tbl_RecordType_Mst obj = new Tbl_RecordType_Mst();
                obj.TenentID = TID;
                obj.RecordType = "socialmedia";
                obj.RecTypeID = DB.Tbl_RecordType_Mst.Where(p => p.TenentID == TID).Count() > 0 ? Convert.ToInt32(DB.Tbl_RecordType_Mst.Where(p => p.TenentID == TID).Max(p => p.RecTypeID) + 1) : 1;
                obj.CompniyAndContactID = COMPID123;
                obj.RecourceName = "Company";
                obj.Recource = Convert.ToInt32(drpSomib.SelectedValue);
                obj.RunSerial = 1;
                obj.RecValue = txtSocial.Text;
                obj.Active = true;
                // obj.Rremark = "AutomatedProcess";
                DB.Tbl_RecordType_Mst.AddObject(obj);
                DB.SaveChanges();
                SoiclMediya(COMPID123);
            }

            else
            {
                string display = "Social Media Is Duplicate!";
                panelMsg.Visible = true;
                lblErreorMsg.Text = display;
                //  ClientScript.RegisterStartupScript(this.GetType(), "Social Media Is Duplicate!", "alert('" + display + "');", true);
                return;
            }

        }


        public void LastData()
        {
            btnSubmit.Visible = false;
            Listview1.SelectedIndex = Listview1.Items.Count - 1;
            txtCustomerName.Text = (Listview1.SelectedDataKey["COMPNAME"]).ToString();
            txtCustomer.Text = (Listview1.SelectedDataKey[1]).ToString();
            txtCustomer2.Text = (Listview1.SelectedDataKey[2]).ToString();
            tags_2.Text = (Listview1.SelectedDataKey[3]).ToString();
            drpItManager.SelectedValue = (Listview1.SelectedDataKey[4]).ToString();
            txtAddress.Text = (Listview1.SelectedDataKey[5]).ToString();
            txtAddress2.Text = (Listview1.SelectedDataKey[6]).ToString();
            txtCity.Text = (Listview1.SelectedDataKey[7]).ToString();
            drpSates.SelectedValue = (Listview1.SelectedDataKey[8]).ToString();
            txtPostalCode.Text = (Listview1.SelectedDataKey[9]).ToString();
            txtZipCode.Text = (Listview1.SelectedDataKey[10]).ToString();
            drpMyCounLocID.SelectedValue = (Listview1.SelectedDataKey[11]).ToString();
            drpMyProductId.SelectedValue = (Listview1.SelectedDataKey[12]).ToString();
            drpCountry.SelectedValue = (Listview1.SelectedDataKey[13]).ToString();
            tags_4.Text = (Listview1.SelectedDataKey[14]).ToString();
            txtMobileNo.Text = (Listview1.SelectedDataKey[15]).ToString();
            tags_3.Text = (Listview1.SelectedDataKey[16]).ToString();
            // int PLID = Convert.ToInt32(objtbl_COMPANYSETUP.PRIMLANGUGE);
            drpPrimaryLang.SelectedValue = (Listview1.SelectedDataKey[17]).ToString();
            txtWebsite.Text = (Listview1.SelectedDataKey[18]).ToString();
            drpType.SelectedValue = (Listview1.SelectedDataKey[19]).ToString();

            //chbIsMinistry.Checked = (Listview1.SelectedDataKey[20]).ToString();
            //chbIssMb.Checked = (Listview1.SelectedDataKey[21]).ToString();
            //chbIsCorporate.Checked = (Listview1.SelectedDataKey[22]).ToString();
            //chbInHawally.Checked = (Listview1.SelectedDataKey[23]).ToString();
            //chbSaler.Checked = (Listview1.SelectedDataKey[24]).ToString();
            //chbBuyer.Checked = (Listview1.SelectedDataKey[25]).ToString();
            //chbSaleDeProd.Checked = (Listview1.SelectedDataKey[26]).ToString();

            tags_5.Text = (Listview1.SelectedDataKey[27]).ToString();
            txtRemark.Text = (Listview1.SelectedDataKey[28]).ToString();
            tags_1.Text = (Listview1.SelectedDataKey[29]).ToString();
            txtcUserName.Text = (Listview1.SelectedDataKey[30]).ToString();
            txtcPassword.Text = (Listview1.SelectedDataKey[31]).ToString();

            // int COID = objtbl_COMPANYSETUP.COUNTRYID;




        }

        protected void btnBared_Click(object sender, EventArgs e)
        {
            string drpval = drpbrand.SelectedItem.ToString();

            if (ViewState["SaveList2"] != null)
            {
                ViewState["SaveList2"] += "/" + drpval;
                tags_5.Text = ViewState["SaveList2"].ToString();
            }
            else
            {
                ViewState["SaveList2"] = drpval;
                tags_5.Text = ViewState["SaveList2"].ToString();
            }
            ViewState["SaveList2"] = tags_5.Text;
        }

        protected void drpCompnay_SelectedIndexChanged(object sender, EventArgs e)
        {

            int CID = Convert.ToInt32(drpCompnay.SelectedValue);
            BindConmpnicontect(CID);
        }
        public void BindConmpnicontect(int CID)
        {
            lstCompniy.DataSource = DB.tblCONTACTBus.Where(p => p.TenentID == TID && p.Active == "Y" && p.ContactMyID == CID);
            lstCompniy.DataBind();
        }

        protected void btnAddCobus_Click(object sender, EventArgs e)
        {
            int CID123 = Convert.ToInt32(ViewState["compId"]);
            int COMPID = 0;
            if (Request.QueryString["COMPID"] != null || CID123 != 0)
            {
                int COMPID13 = Convert.ToInt32(Request.QueryString["COMPID"]);
                if (COMPID13 != null && COMPID13 != 0)
                    COMPID = COMPID13;
                else
                    COMPID = CID123;
            }
            else
                COMPID = DB.TBLCOMPANYSETUPs.Where(p => p.TenentID == TID).Count() > 0 ? Convert.ToInt32(DB.TBLCOMPANYSETUPs.Where(p => p.TenentID == TID).Max(p => p.COMPID) + 1) : 1;


            int CID = Convert.ToInt32(drpCompnay.SelectedValue);
            var obj = DB.TBLCONTACTs.Single(p => p.TenentID == TID && p.ContactMyID == CID);
            if (ViewState["ListtblCONTACTBus"] != null)
            {
                ListtblCONTACTBus = (List<Database.tblCONTACTBu>)ViewState["ListtblCONTACTBus"];
            }
            tblCONTACTBu LIst = new tblCONTACTBu();
            //  Database.tblCONTACTBus objtbl_CONTACTBus = new Database.tblCONTACTBus();
            LIst.TenentID = TID;
            LIst.BusPhone1 = obj.BUSPHONE1;
            LIst.ContactMyID = CID;
            LIst.CompID = COMPID;
            LIst.Country = obj.COUNTRYID.ToString();
            LIst.email2 = obj.EMAIL1;
           // LIst.Fax = obj.Fax;
            LIst.JobTitle = drpItManager.SelectedValue;
            LIst.PhysicalLocID = obj.PHYSICALLOCID;
            LIst.remarks = obj.REMARKS;
            LIst.Active = "Y";
            ListtblCONTACTBus.Add(LIst);
            ViewState["ListtblCONTACTBus"] = ListtblCONTACTBus;
            BindtimeData();
        }
        public void BindtimeData()
        {
            ListtblCONTACTBus = ((List<Database.tblCONTACTBu>)ViewState["ListtblCONTACTBus"]).ToList();
            lstCompniy.DataSource = ListtblCONTACTBus;
            lstCompniy.DataBind();
        }

        protected void btnSearchSave_Click(object sender, EventArgs e)
        {
            string RefType = Convert.ToString(SearchManagement.SearchType.Search);
            string RefSubType = Convert.ToString(SearchManagement.SearchType.Company.ToString());

            if (Convert.ToInt32(DrpTitle.SelectedValue) == 0)
            {
                if (Listview1.Items.Count() > 0)
                {
                    if (txtTitle.Text != "")
                    {
                        List<Database.REFTABLE> List_Ref = DB.REFTABLEs.ToList();
                        if (List_Ref.Where(p => p.REFNAME1 == txtTitle.Text && p.REFTYPE == RefType && p.REFSUBTYPE == RefSubType && p.TenentID == TID).Count() < 1)
                        {
                            int RID = List_Ref.Where(p => p.TenentID == TID).Count() > 0 ? Convert.ToInt32(List_Ref.Where(p => p.TenentID == TID).Max(p => p.REFID) + 1) : 1;
                            int Con = 0;
                            bool flag = false;
                            for (int i = 0; i < Listview1.Items.Count; i++)
                            {
                                HiddenField HID = (HiddenField)Listview1.Items[i].FindControl("hidecontactid");
                                Con = Convert.ToInt32(HID.Value);
                                Database.ISSearchDetail obj_Detail = new ISSearchDetail();
                                obj_Detail.ID = DB.ISSearchDetails.Where(p => p.TenentID == TID).Count() > 0 ? Convert.ToInt32(DB.ISSearchDetails.Where(p => p.TenentID == TID).Max(p => p.ID) + 1) : 1;
                                obj_Detail.TenentID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
                                obj_Detail.LocationID = 1;
                                obj_Detail.REFID = RID;
                                obj_Detail.CompanyID = Con;
                                obj_Detail.CreatedBy = UID;
                                obj_Detail.Active = true;
                                obj_Detail.Deleted = true;
                                if (DB.ISSearchDetails.Where(p => p.CompanyID == Con && p.CreatedBy == UID && p.REFID == RID && p.TenentID == TID).Count() < 1)
                                {
                                    DB.ISSearchDetails.AddObject(obj_Detail);
                                    DB.SaveChanges();
                                    flag = true;
                                }

                            }
                            //foreach (ListViewItem item in Listview1.Items)
                            //{
                            //    HiddenField HID = (HiddenField)item.FindControl("hidecontactid");
                            //    Con = Convert.ToInt32(HID.Value);
                            //    Database.ISSearchDetail obj_Detail = new ISSearchDetail();
                            //    obj_Detail.ID = DB.ISSearchDetail.Count() > 0 ? Convert.ToInt32(DB.ISSearchDetail.Max(p => p.ID) + 1) : 1;
                            //    obj_Detail.TenentID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
                            //    obj_Detail.LocationID = 1;
                            //    obj_Detail.REFID = RID;
                            //    obj_Detail.ContactCompanyID = Con;
                            //    obj_Detail.CreatedBy = UID;
                            //    obj_Detail.Active = true;
                            //    obj_Detail.Deleted = true;
                            //    if (DB.ISSearchDetail.Where(p => p.ContactCompanyID == Con && p.CreatedBy == UID).Count() < 1)
                            //    {
                            //        DB.ISSearchDetail.AddObject(obj_Detail);
                            //        DB.SaveChanges();
                            //    }
                            //    else
                            //    {
                            //        flag = true;
                            //    }

                            //}
                            if (flag == true)
                            {
                                Database.REFTABLE obj_Ref = new Database.REFTABLE();
                                obj_Ref.TenentID = TID;
                                obj_Ref.REFID = RID;
                                obj_Ref.REFTYPE = RefType;
                                obj_Ref.REFSUBTYPE = RefSubType;
                                obj_Ref.SHORTNAME = RefType;
                                obj_Ref.REFNAME1 = txtTitle.Text;
                                obj_Ref.REFNAME2 = txtTitle.Text;
                                obj_Ref.REFNAME3 = txtTitle.Text;
                                obj_Ref.ACTIVE = "Y";
                                DB.REFTABLEs.AddObject(obj_Ref);
                                DB.SaveChanges();
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Data Save Sucessfully...');", true);
                            }

                        }
                        else
                        {

                            ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Title Name Already Exist...');", true);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Must Enter Title...');", true);

                    }

                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('No Search Found...');", true);
                }
            }
            BindTitleData();
            txtTitle.Text = "";
        }
        public void BindTitleData()
        {
            string REFtype = Convert.ToString(SearchManagement.SearchType.Search);
            string REFSubtype = Convert.ToString(SearchManagement.SearchType.Company);
            DrpTitle.DataSource = DB.REFTABLEs.Where(P => P.REFTYPE == REFtype && P.REFSUBTYPE == REFSubtype && P.TenentID == TID && P.ACTIVE == "Y");
            DrpTitle.DataTextField = "REFNAME1";
            DrpTitle.DataValueField = "REFID";
            DrpTitle.DataBind();
            DrpTitle.Items.Insert(0, new ListItem("-- Select --", "0"));
        }
        public string getContactName(int ID)
        {
            int ConID = Convert.ToInt32(ID);
            string Name = "";
            if (DB.TBLCONTACTs.Where(p => p.ContactMyID == ConID && p.TenentID == TID).Count() > 0)
            {
                Database.TBLCONTACT obj_con = DB.TBLCONTACTs.Single(p => p.ContactMyID == ConID && p.TenentID == TID);
                Name = obj_con.PersName1;
            }
            return Name;
        }

      

        protected void btnlistreload_Click(object sender, EventArgs e)
        {
            BindData();
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

  
        protected void LinkButton11_Click(object sender, EventArgs e)
        {
            Response.Redirect("CompanySearch.aspx");
        }

        protected void btnFirst_Click1(object sender, EventArgs e)
        {
            //int COMID = DB.TBLCOMPANYSETUP.Where(p => p.Active == "Y").Min(p => p.COMPID);
            //BindEditcompniy(COMID);
            //redonlyfalse();

            int LIDID = DB.TBLCOMPANYSETUPs.Where(p => p.Active == "Y" && p.TenentID == TID).FirstOrDefault().COMPID;
            Listview1.SelectedIndex = LIDID;
            BindEditcompniy(LIDID);
            redonlyfalse();
        }

        protected void btnNext_Click(object sender, EventArgs e)
        {
            if (Listview1.SelectedIndex != Listview1.Items.Count - 1)
            {
                Listview1.SelectedIndex = Listview1.SelectedIndex + 1;
                int LIDID = Convert.ToInt32(Listview1.SelectedDataKey[0]);
                Listview1.SelectedIndex = LIDID;
                BindEditcompniy(LIDID);
                redonlyfalse();
            }
        }

        protected void btnPrev_Click(object sender, EventArgs e)
        {
            if (Listview1.SelectedIndex == 0)
            {
                //lblMsg.Text = "This is first record";
                //pnlSuccessMsg.Visible = true;

            }
            else
            {
                Listview1.SelectedIndex = Listview1.SelectedIndex - 1;
                int LIDID = Convert.ToInt32(Listview1.SelectedDataKey[0]);
                //    LIDID = Convert.ToInt32(Listview1.SelectedIndex);

                Listview1.SelectedIndex = LIDID;
                BindEditcompniy(LIDID);
                redonlyfalse();
            }
        }

        protected void btnLast_Click(object sender, EventArgs e)
        {
            int LIDID = DB.TBLCOMPANYSETUPs.Where(p => p.Active == "Y" && p.TenentID == TID).Max(p => p.COMPID);

            BindEditcompniy(LIDID);
            Listview1.SelectedIndex = LIDID;
            redonlyfalse();
            //int COMID = DB.TBLCOMPANYSETUP.Where(p => p.Active == "Y").Max(p => p.COMPID);
            //BindEditcompniy(COMID);
            //redonlyfalse();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            redonlyture();
            btnSubmit.Visible = true;
            btnSubmit.Text = "Save";
            clen();
            Button1.Visible = false;
        }

        protected void btnAppend_Click(object sender, EventArgs e)
        {
            int ID = Convert.ToInt32(DrpTitle.SelectedValue);
            int Con = 0;
            for (int i = 0; i < Listview1.Items.Count; i++)
            {
                HiddenField HID = (HiddenField)Listview1.Items[i].FindControl("hidecontactid");
                Con = Convert.ToInt32(HID.Value);
                Database.ISSearchDetail obj_Detail = new ISSearchDetail();
                obj_Detail.ID = DB.ISSearchDetails.Where(p => p.TenentID == TID).Count() > 0 ? Convert.ToInt32(DB.ISSearchDetails.Where(p => p.TenentID == TID).Max(p => p.ID) + 1) : 1;
                obj_Detail.TenentID = TID;
                obj_Detail.LocationID = 1;
                obj_Detail.REFID = ID;
                obj_Detail.CompanyID = Con;
                obj_Detail.CreatedBy = UID;
                obj_Detail.Active = true;
                obj_Detail.Deleted = true;
                if (DB.ISSearchDetails.Where(p => p.CompanyID == Con && p.CreatedBy == UID && p.REFID == ID && p.TenentID == TID).Count() < 1)
                {
                    DB.ISSearchDetails.AddObject(obj_Detail);
                    DB.SaveChanges();
                    flag = true;
                }

            }
        }
        public string Translate(string textvalue, string to)
        {
            string appId = "A70C584051881A30549986E65FF4B92B95B353A5";//go to http://msdn.microsoft.com/en-us/library/ff512386.aspx to obtain AppId.
            // string textvalue = "Translate this for me";
            string from = "en";

            string uri = "http://api.microsofttranslator.com/v2/Http.svc/Translate?appId=" + appId + "&text=" + textvalue + "&from=" + from + "&to=" + to;
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(uri);
            WebResponse response = null;
            try
            {
                response = httpWebRequest.GetResponse();
                using (Stream stream = response.GetResponseStream())
                {
                    System.Runtime.Serialization.DataContractSerializer dcs = new System.Runtime.Serialization.DataContractSerializer(Type.GetType("System.String"));
                    string translation = (string)dcs.ReadObject(stream);
                    return translation;
                }
            }
            catch (WebException e)
            {
                return "";
            }
            finally
            {
                if (response != null)
                {
                    response.Close();
                    response = null;
                }
            }
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(DrpTitle.SelectedValue) != 0)
            {
                int TitleID = Convert.ToInt32(DrpTitle.SelectedValue);

                List<ISSearchDetail> Search_List = DB.ISSearchDetails.Where(p => p.REFID == TitleID && p.CreatedBy == UID && p.TenentID == TID).ToList();
                List<Database.TBLCOMPANYSETUP> Con_List = new List<Database.TBLCOMPANYSETUP>();
                //TBLCOMPANYSETUP.Where(p => p.TenentID == TID && p.Approved == 0);

                foreach (ISSearchDetail item in Search_List)
                {
                    Database.TBLCOMPANYSETUP obj_Contact = DB.TBLCOMPANYSETUPs.Single(p => p.COMPID == item.CompanyID && p.TenentID == TID);
                    Con_List.Add(obj_Contact);
                }
                var List = Con_List.OrderBy(p => p.UPDTTIME).ToList();
                ViewState["SaveList"] = List;
                ViewState["SerchListofCompny"] = List;
                //int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
                //int Totalrec = List.Count();
                //((CRMMaster)Page.Master).Loadlist(Showdata, take, Skip, ChoiceID, lblShowinfEntry, btnPrevious1, btnNext1, Listview1, ListView2, Totalrec, List);
                Listview1.DataSource = List;//DB.TBLCONTACT.Where(p => p.TenentID == TID && p.ContactMyID==TitleID && p.Active == "Y" && p.PHYSICALLOCID != "HLY");
                Listview1.DataBind();

            }
        }
        public string getjobititel(int JID)
        {
            if (JID != 0)
                return DB.Tbl_Position_Mst.Single(p => p.PositionID == JID && p.TenentID == TID).PositionName;
            else
                return "";
        }

        protected void LinkButton12_Click(object sender, EventArgs e)
        {
            string id1 = txtcompneySerch.Text.ToString();
            List<TBLCONTACT> list1 = DB.TBLCONTACTs.Where(p =>p.TenentID==TID&& (p.PersName1.ToUpper().Contains(id1.ToUpper()) || p.EMAIL1.ToUpper().Contains(id1.ToUpper()) || p.ADDR1.ToUpper().Contains(id1.ToUpper()) || p.CITY.ToUpper().Contains(id1.ToUpper()) || p.STATE.ToUpper().Contains(id1.ToUpper()) || p.REMARKS.ToUpper().Contains(id1.ToUpper()) || p.MOBPHONE.ToUpper().Contains(id1.ToUpper())) && p.Active == "Y").OrderBy(p => p.PersName1).ToList();
            drpCompnay.DataSource = list1;
            drpCompnay.DataTextField = "PersName1";
            drpCompnay.DataValueField = "ContactMyID";
            drpCompnay.DataBind();
            drpCompnay.Items.Insert(0, new ListItem("-- Select --", "0"));
            int Count = list1.Count();
            if (Count > 0)
                lblcountserch.Text = Count + " Records Founds from Drop Down";
            else
                lblcountserch.Text = " No Records Founds";
        }

        protected void LinkButton13_Click(object sender, EventArgs e)
        {
            string UID = ((USER_MST)Session["USER"]).LOGIN_ID;
            TBLCOMPANYSETUP List = new TBLCOMPANYSETUP();
            List.TenentID = TID;
            List.COMPID = DB.TBLCOMPANYSETUPs.Where(p => p.TenentID == TID).Count() > 0 ? Convert.ToInt32(DB.TBLCOMPANYSETUPs.Where(p => p.TenentID == TID).Max(p => p.COMPID) + 1) : 1;
            ViewState["CIDRF"] = Convert.ToInt32(List.COMPID);
            List.PHYSICALLOCID = txtCustomerName.Text;
            List.COMPNAME1 = txtCustomerName.Text;
            List.COMPNAME2 = txtCustomer.Text;
            List.COMPNAME3 = txtCustomer2.Text;
            List.EMAIL1 = tags_2.Text;
            // objtbl_COMPANYSETUP.EMAIL2 = tags_2.Text;
            List.ITMANAGER = drpItManager.SelectedValue;
            List.ADDR1 = txtAddress.Text;
            List.ADDR2 = txtAddress2.Text;
            List.CITY = txtCity.Text;
            List.STATE = drpSates.SelectedValue;
            List.POSTALCODE = txtPostalCode.Text;
            List.ZIPCODE = txtZipCode.Text;
            List.MYCONLOCID = Convert.ToInt32(drpMyCounLocID.SelectedValue);
            List.MYPRODID = Convert.ToInt32(drpMyProductId.SelectedValue);
            //   string CNAME = drpCountry.SelectedItem.ToString();
            List.COUNTRYID = Convert.ToInt32(drpCountry.SelectedValue);
            List.BUSPHONE1 = tags_4.Text;
            //objtbl_COMPANYSETUP.BUSPHONE2 = tags_4.Text;
            //objtbl_COMPANYSETUP.BUSPHONE3 = tags_4.Text;
            //objtbl_COMPANYSETUP.BUSPHONE4 = tags_4.Text;
            List.MOBPHONE = txtMobileNo.Text;
            List.FAX = tags_3.Text;
            //  objtbl_COMPANYSETUP.FAX2 = tags_3.Text;
            List.PRIMLANGUGE = drpPrimaryLang.SelectedValue;
            List.WEBPAGE = txtWebsite.Text;
            List.CompanyType = drpType.SelectedValue;
            List.ISMINISTRY = chbIsMinistry.Checked ? true : false;
            List.ISSMB = chbIssMb.Checked ? true : false;
            List.ISCORPORATE = chbIsCorporate.Checked ? true : false;
            List.INHAWALLY = chbInHawally.Checked ? true : false;
            List.SALER = chbSaler.Checked ? true : false;
            List.BUYER = chbBuyer.Checked ? true : false;
            List.SALEDEPROD = chbSaleDeProd.Checked ? true : false;
            List.EMAISUB = chbEmailSub.Checked ? true : false;
            List.EMAILSUBDATE = DateTime.Now;
            List.PRODUCTDEALIN = tags_5.Text;
            List.REMARKS = txtRemark.Text;
            List.Keyword = tags_1.Text;
            List.CUSERID = txtcUserName.Text;
            List.CPASSWRD = txtcPassword.Text;
            List.USERID = UID;
            List.ENTRYDATE = DateTime.Now;
            List.ENTRYTIME = DateTime.Now;
            List.UPDTTIME = DateTime.Now;
            List.Approved = 0;
            ListTBLCOMPANYSETUP.Add(List);
            Session["ListTBLCOMPANYSETUP"] = ListTBLCOMPANYSETUP;
            Response.Redirect("ContactMaster.aspx?AddContect=New");
        }
        protected void btnOpportunity_Click(object sender, EventArgs e)
        {
            int CID123 = Convert.ToInt32(ViewState["compId"]);
            int COMPID = 0;
            if (Request.QueryString["COMPID"] != null || CID123 != 0)
            {
                int COMPID13 = Convert.ToInt32(Request.QueryString["COMPID"]);
                if (COMPID13 != null && COMPID13 != 0)
                    COMPID = COMPID13;
                else
                    COMPID = CID123;
            }
            else
                COMPID = DB.TBLCOMPANYSETUPs.Where(p => p.TenentID == TID).Count() > 0 ? Convert.ToInt32(DB.TBLCOMPANYSETUPs.Where(p => p.TenentID == TID).Max(p => p.COMPID) + 1) : 1;
            string Mode = ViewState["ModeData"].ToString();
            string URL = "AttachmentMst.aspx?AttachID=" + COMPID + "&DID=CompanyMaster&Mode=" + Mode;
            Response.Redirect(URL);
        }

        protected void libtnNewClass_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/ECOMM/CategoryMaster");
        }

        protected void drpCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            string drpval = drpCategory.SelectedItem.ToString();
            if (ViewState["SaveList1"] != null)
            {
                ViewState["SaveList1"] += "," + drpval;
                tags_1.Text = ViewState["SaveList1"].ToString();
            }
            else
            {
                ViewState["SaveList1"] = drpval;
                tags_1.Text = ViewState["SaveList1"].ToString();
            }
            ViewState["SaveList1"] = tags_1.Text;

        }

        protected void btnMinClassfication_Click(object sender, EventArgs e)
        {
            if (DB.CAT_MST.Where(p => p.CAT_NAME1 == txtclassficname.Text && p.PARENT_CATID == 7861 && p.CAT_TYPE == "COMP" && p.TenentID == TID).Count() == 0)
            {
                Database.CAT_MST objcat = new Database.CAT_MST();
                objcat.CATID = DB.CAT_MST.Where(p => p.TenentID == TID).Count() > 0 ? Convert.ToInt32(DB.CAT_MST.Where(p => p.TenentID == TID).Max(p => p.CATID) + 1) : 1;
                objcat.TenentID = 1;
                objcat.CAT_NAME1 = txtclassficname.Text;
                objcat.CAT_NAME2 = txtclassficname.Text;
                objcat.CAT_NAME3 = txtclassficname.Text;
                objcat.CAT_DESCRIPTION = txtclassficname.Text;
                objcat.SHORT_NAME = txtclassficname.Text;
                // objcat.WARRANTY = txtWarranty.Text;
                objcat.PARENT_CATID = 7861;
                objcat.CAT_TYPE = "COMP";
                objcat.Active = "1";
                DB.CAT_MST.AddObject(objcat);
                DB.SaveChanges();
            }
            else
            {

            }




            BindDataClassfic();
        }

        protected void drpCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            string CID = drpCountry.SelectedValue;
            bindSates(CID);
        }


        public void bindSates(string CID)
        {
            Classes.EcommAdminClass.getdropdown(drpSates, TID, CID, "", "", "tblStates");
            //select * from tblStates where Active= 'Y'

            //drpSates.DataSource = DB.tblStates.Where(P => P.COUNTRYID == CID);
            //drpSates.DataTextField = "MYNAME1";
            //drpSates.DataValueField = "StateID";
            //drpSates.DataBind();
            //drpSates.Items.Insert(0, new ListItem("-- Select --", "0"));
        }

        


    }
}