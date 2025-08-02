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
using System.Net.Mail;
using System.IO;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Text;
using Web.CRM.Class.Class;
using System.Drawing;
using GenCode128;
using QRCoder;
using System.Web.Services;
using System.ComponentModel;
using System.Web.SessionState;
using Classes;


namespace Web.CRM
{
    public partial class CompanyMaster : System.Web.UI.Page
    {
        //for Excel to import
        OleDbConnection Econ;
        SqlConnection con1;
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CRMNewEntitiesNew"].ConnectionString);
        SqlCommand command2 = new SqlCommand();
        //for Excel to import End

        int count = 0;
        int take = 0;
        int Skip = 0;
        public static int ChoiceID = 0;


        #region Code patterns

        // in principle these rows should each have 6 elements
        // however, the last one -- STOP -- has 7. The cost of the
        // extra integers is trivial, and this lets the code flow
        // much more elegantly
        private static readonly int[,] cPatterns = 
                     {
                        {2,1,2,2,2,2,0,0},  // 0
                        {2,2,2,1,2,2,0,0},  // 1
                        {2,2,2,2,2,1,0,0},  // 2
                        {1,2,1,2,2,3,0,0},  // 3
                        {1,2,1,3,2,2,0,0},  // 4
                        {1,3,1,2,2,2,0,0},  // 5
                        {1,2,2,2,1,3,0,0},  // 6
                        {1,2,2,3,1,2,0,0},  // 7
                        {1,3,2,2,1,2,0,0},  // 8
                        {2,2,1,2,1,3,0,0},  // 9
                        {2,2,1,3,1,2,0,0},  // 10
                        {2,3,1,2,1,2,0,0},  // 11
                        {1,1,2,2,3,2,0,0},  // 12
                        {1,2,2,1,3,2,0,0},  // 13
                        {1,2,2,2,3,1,0,0},  // 14
                        {1,1,3,2,2,2,0,0},  // 15
                        {1,2,3,1,2,2,0,0},  // 16
                        {1,2,3,2,2,1,0,0},  // 17
                        {2,2,3,2,1,1,0,0},  // 18
                        {2,2,1,1,3,2,0,0},  // 19
                        {2,2,1,2,3,1,0,0},  // 20
                        {2,1,3,2,1,2,0,0},  // 21
                        {2,2,3,1,1,2,0,0},  // 22
                        {3,1,2,1,3,1,0,0},  // 23
                        {3,1,1,2,2,2,0,0},  // 24
                        {3,2,1,1,2,2,0,0},  // 25
                        {3,2,1,2,2,1,0,0},  // 26
                        {3,1,2,2,1,2,0,0},  // 27
                        {3,2,2,1,1,2,0,0},  // 28
                        {3,2,2,2,1,1,0,0},  // 29
                        {2,1,2,1,2,3,0,0},  // 30
                        {2,1,2,3,2,1,0,0},  // 31
                        {2,3,2,1,2,1,0,0},  // 32
                        {1,1,1,3,2,3,0,0},  // 33
                        {1,3,1,1,2,3,0,0},  // 34
                        {1,3,1,3,2,1,0,0},  // 35
                        {1,1,2,3,1,3,0,0},  // 36
                        {1,3,2,1,1,3,0,0},  // 37
                        {1,3,2,3,1,1,0,0},  // 38
                        {2,1,1,3,1,3,0,0},  // 39
                        {2,3,1,1,1,3,0,0},  // 40
                        {2,3,1,3,1,1,0,0},  // 41
                        {1,1,2,1,3,3,0,0},  // 42
                        {1,1,2,3,3,1,0,0},  // 43
                        {1,3,2,1,3,1,0,0},  // 44
                        {1,1,3,1,2,3,0,0},  // 45
                        {1,1,3,3,2,1,0,0},  // 46
                        {1,3,3,1,2,1,0,0},  // 47
                        {3,1,3,1,2,1,0,0},  // 48
                        {2,1,1,3,3,1,0,0},  // 49
                        {2,3,1,1,3,1,0,0},  // 50
                        {2,1,3,1,1,3,0,0},  // 51
                        {2,1,3,3,1,1,0,0},  // 52
                        {2,1,3,1,3,1,0,0},  // 53
                        {3,1,1,1,2,3,0,0},  // 54
                        {3,1,1,3,2,1,0,0},  // 55
                        {3,3,1,1,2,1,0,0},  // 56
                        {3,1,2,1,1,3,0,0},  // 57
                        {3,1,2,3,1,1,0,0},  // 58
                        {3,3,2,1,1,1,0,0},  // 59
                        {3,1,4,1,1,1,0,0},  // 60
                        {2,2,1,4,1,1,0,0},  // 61
                        {4,3,1,1,1,1,0,0},  // 62
                        {1,1,1,2,2,4,0,0},  // 63
                        {1,1,1,4,2,2,0,0},  // 64
                        {1,2,1,1,2,4,0,0},  // 65
                        {1,2,1,4,2,1,0,0},  // 66
                        {1,4,1,1,2,2,0,0},  // 67
                        {1,4,1,2,2,1,0,0},  // 68
                        {1,1,2,2,1,4,0,0},  // 69
                        {1,1,2,4,1,2,0,0},  // 70
                        {1,2,2,1,1,4,0,0},  // 71
                        {1,2,2,4,1,1,0,0},  // 72
                        {1,4,2,1,1,2,0,0},  // 73
                        {1,4,2,2,1,1,0,0},  // 74
                        {2,4,1,2,1,1,0,0},  // 75
                        {2,2,1,1,1,4,0,0},  // 76
                        {4,1,3,1,1,1,0,0},  // 77
                        {2,4,1,1,1,2,0,0},  // 78
                        {1,3,4,1,1,1,0,0},  // 79
                        {1,1,1,2,4,2,0,0},  // 80
                        {1,2,1,1,4,2,0,0},  // 81
                        {1,2,1,2,4,1,0,0},  // 82
                        {1,1,4,2,1,2,0,0},  // 83
                        {1,2,4,1,1,2,0,0},  // 84
                        {1,2,4,2,1,1,0,0},  // 85
                        {4,1,1,2,1,2,0,0},  // 86
                        {4,2,1,1,1,2,0,0},  // 87
                        {4,2,1,2,1,1,0,0},  // 88
                        {2,1,2,1,4,1,0,0},  // 89
                        {2,1,4,1,2,1,0,0},  // 90
                        {4,1,2,1,2,1,0,0},  // 91
                        {1,1,1,1,4,3,0,0},  // 92
                        {1,1,1,3,4,1,0,0},  // 93
                        {1,3,1,1,4,1,0,0},  // 94
                        {1,1,4,1,1,3,0,0},  // 95
                        {1,1,4,3,1,1,0,0},  // 96
                        {4,1,1,1,1,3,0,0},  // 97
                        {4,1,1,3,1,1,0,0},  // 98
                        {1,1,3,1,4,1,0,0},  // 99
                        {1,1,4,1,3,1,0,0},  // 100
                        {3,1,1,1,4,1,0,0},  // 101
                        {4,1,1,1,3,1,0,0},  // 102
                        {2,1,1,4,1,2,0,0},  // 103
                        {2,1,1,2,1,4,0,0},  // 104
                        {2,1,1,2,3,2,0,0},  // 105
                        {2,3,3,1,1,1,2,0}   // 106
                     };

        #endregion Code patterns
        private const int cQuietWidth = 10;
        Database.CallEntities DB = new Database.CallEntities();
        //for Excel to import
        private SqlConnection con2;
        private SqlCommand com;
        private string constr1, query;

        private void connection1()
        {
            constr1 = ConfigurationManager.ConnectionStrings["SqlCom"].ToString();
            con2 = new SqlConnection(constr1);
            con2.Open();
        }
        //for Excel to import End

        List<CAT_MST> List = new List<CAT_MST>();
        List<TBLCOMPANYSETUP> ListTBLCOMPANYSETUP = new List<TBLCOMPANYSETUP>();
        List<tblCONTACTBu> ListtblCONTACTBus = new List<tblCONTACTBu>();
        bool flag = false;
        string languageId = "";
        bool FirstFlag, ClickFlag = true;
        int TID, LID, stMYTRANSID, UID, MID, EMPID, Transid, Transsubid, CID = 0;
        string LangID, CURRENCY, USERID, Crypath = "";

        List<Navigation> ChoiceList = new List<Navigation>();
        List<Database.TBLCOMPANYSETUP> ListCompsetup = new List<TBLCOMPANYSETUP>();


        protected void Page_Load(object sender, EventArgs e)
        {
            panelMsg.Visible = false;
            pnlSuccessMsg.Visible = false;
            PanelError.Visible = false;
            lblMsg.Text = "";
            lblerror.Text = "";
            SessionLoad();
            if (TID == 9)
            {
                btnattmentmst.Attributes["Style"] = "display:none;";
                toolss.Attributes["Style"] = "display:none;";
                LinkTools.Attributes["Style"] = "display:none;";
                attechment.Attributes["Style"] = "display:none;";
                LinkButton11.Attributes["Style"] = "display:none;";
            }
            if (TID == 900)
            {
                btnattmentmst.Attributes["Style"] = "display:none;";
                toolss.Attributes["Style"] = "display:none;";
                LinkTools.Attributes["Style"] = "display:none;";
                attechment.Attributes["Style"] = "display:none;";
                LinkButton11.Attributes["Style"] = "display:none;";
            }

            if (!IsPostBack)
            {
                Setpanel("none", "expand");
                txtCustoID.Enabled = false;
                FistTimeLoad();
                ViewState["SaveList1"] = null;
                Session["Pagename"] = "Suppliers";

                FillContractorID();

                redonlyfalse();
                BindTitleData();
                if (DB.CRMUserSetups.Where(p => p.TenentID == TID && p.userid == UID && p.startupCompRefID != 0).Count() > 0)
                {

                    Database.CRMUserSetup objCRM = DB.CRMUserSetups.Single(p => p.TenentID == TID && p.userid == UID && p.startupCompRefID != 0);
                    chDefaultSet.Checked = objCRM.Active == true ? true : false;
                    if (objCRM.Active == true)
                    {
                        DrpTitle.SelectedValue = objCRM.startupCompRefID.ToString();
                        SetDefaultGrid();
                    }

                }
                //   TabName.Value = Request.Form[TabName.UniqueID];
                if (flag == false)
                {
                    flag = true;
                }
                if (Request.QueryString["Sesstion"] != null)
                {
                    if (Session["SerchList"] != null)
                    {
                        var List = ((List<Database.TBLCOMPANYSETUP>)Session["SerchList"]).ToList();

                        //Listview1.DataSource = List;
                        //Listview1.DataBind();

                        int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
                        int Countt = DB.TBLCOMPANYSETUPs.Where(p => p.TenentID == TID).Count();
                        int Totalrec = Countt;

                        BindEditcompniy(List[0].COMPID);
                        Loadlist(Showdata, take, Skip, ChoiceID, lblShowinfEntry, btnPrevious1, btnNext1, Listview1, ListView2, Totalrec, List);
                    }
                }
                if (Request.QueryString["COMPID"] != null)
                {
                    if (Request.QueryString["DropFun"] == null)
                    {
                        int ContactMyID = Convert.ToInt32(Request.QueryString["COMPID"]);
                        txtCustoID.Text = ContactMyID.ToString();
                        lblAttecmentcount.Text = Countatt(ContactMyID);
                        btnattmentmst.Visible = true;
                        PanalFBImage.Visible = true;
                        string Custname = GetCustomerName(ContactMyID);
                        if (Request.QueryString["Mode"] != null)
                        {
                            string Mode = Request.QueryString["Mode"].ToString();
                            if (Mode == "Write")
                            {
                                redonlyture();
                                btnSubmit.Text = "Update";
                                btnSubmit.Visible = true;
                                Button1.Visible = false;
                                lblBusContactDe.Text = "Business Company Details - (Write Mode) -" + Custname;
                                lblBCDeta.Text = "Business Company Details - (Write Mode) -" + Custname;
                                lblwExistance.Text = "Web Existance - (Write Mode) -" + Custname;
                                lblWEmp.Text = "Working Employees - (Write Mode) -" + Custname;
                            }
                            else
                            {

                                redonlyfalse();
                                btnSubmit.Visible = false;
                                Button1.Visible = true;
                                lblAttecmentcount.Text = Countatt(ContactMyID);
                                btnattmentmst.Visible = true;
                                PanalFBImage.Visible = true;
                                lblBusContactDe.Text = "Business Company Details -  (Read Mode) -" + Custname;
                                lblBCDeta.Text = "Business Company Details -  (Read Mode) -" + Custname;
                                lblwExistance.Text = "Web Existance -  (Read Mode) -" + Custname;
                                lblWEmp.Text = "Working Employees -  (Read Mode) -" + Custname;

                            }
                            var List = DB.TBLCOMPANYSETUPs.Where(p => p.COMPID == ContactMyID && p.TenentID == TID).ToList();
                            ViewState["SaveList"] = List;
                            int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
                            int Totalrec = List.Count();

                            BindEditcompniy(List[0].COMPID);
                            Loadlist(Showdata, take, Skip, ChoiceID, lblShowinfEntry, btnPrevious1, btnNext1, Listview1, ListView2, Totalrec, List);
                            //Listview1.DataSource = List;
                            //Listview1.DataBind();
                        }
                        else
                        {
                            BindEditcompniy(ContactMyID);
                        }
                    }
                }
                else
                {
                    firstdata();
                }

                if (Request.QueryString["NewContect"] != null)
                {

                    if (Session["ListTBLCOMPANYSETUP"] != null)
                    {
                        ListTBLCOMPANYSETUP = (List<Database.TBLCOMPANYSETUP>)Session["ListTBLCOMPANYSETUP"];
                        //LinkButton13.Visible = true;
                        int ContactMyID = Convert.ToInt32(ListTBLCOMPANYSETUP[0].COMPID);
                        if (Request.QueryString["COMPID"] != null)
                        {
                            ContactMyID.ToString();
                            txtCustoID.Text = ContactMyID.ToString();
                            lblAttecmentcount.Text = Countatt(ContactMyID);
                            btnattmentmst.Visible = true;
                            PanalFBImage.Visible = true;
                            string Custname = GetCustomerName(ContactMyID);
                            string Mode = "Write";
                            if (Mode != null && Mode != "")
                            {
                                if (Mode == "Write")
                                {
                                    redonlyture();
                                    btnSubmit.Text = "Update";
                                    btnSubmit.Visible = true;
                                    Button1.Visible = false;
                                    lblBusContactDe.Text = "Business Company Details - (Write Mode) -" + Custname;
                                    lblBCDeta.Text = "Business Company Details - (Write Mode) -" + Custname;
                                    lblwExistance.Text = "Web Existance - (Write Mode) -" + Custname;
                                    lblWEmp.Text = "Working Employees - (Write Mode) -" + Custname;
                                }

                                var List = DB.TBLCOMPANYSETUPs.Where(p => p.COMPID == ContactMyID && p.TenentID == TID).ToList();
                                ViewState["SaveList"] = List;
                                int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
                                int Totalrec = List.Count();

                                BindEditcompniy(List[0].COMPID);
                                Loadlist(Showdata, take, Skip, ChoiceID, lblShowinfEntry, btnPrevious1, btnNext1, Listview1, ListView2, Totalrec, List);

                            }
                        }
                        else
                        {
                            txtCustoID.Text = ListTBLCOMPANYSETUP[0].COMPID.ToString();
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
                            txtBirthdate.Text = ListTBLCOMPANYSETUP[0].BirthDate.ToString();
                            txtCivilID.Text = ListTBLCOMPANYSETUP[0].CivilID;
                            //txtCity.Text = ListTBLCOMPANYSETUP[0].CITY;

                            if (ListTBLCOMPANYSETUP[0].CITY != null && Convert.ToInt32(ListTBLCOMPANYSETUP[0].CITY) != 0)
                            {
                                drpcity.SelectedValue = (ListTBLCOMPANYSETUP[0].CITY).ToString();
                            }

                            txtPostalCode.Text = ListTBLCOMPANYSETUP[0].POSTALCODE;
                            txtZipCode.Text = ListTBLCOMPANYSETUP[0].ZIPCODE;
                            // drpMyCounLocID.SelectedValue = ListTBLCOMPANYSETUP[0].MYCONLOCID.ToString();
                            if (ListTBLCOMPANYSETUP[0].MYPRODID != null && ListTBLCOMPANYSETUP[0].MYPRODID != 0)
                                drpMyProductId.SelectedValue = ListTBLCOMPANYSETUP[0].MYPRODID.ToString();

                            tags_4.Text = ListTBLCOMPANYSETUP[0].BUSPHONE1;
                            txtMobileNo.Text = ListTBLCOMPANYSETUP[0].MOBPHONE;

                            tags_3.Text = ListTBLCOMPANYSETUP[0].FAX;
                            int PLID = Convert.ToInt32(ListTBLCOMPANYSETUP[0].PRIMLANGUGE);
                            //drpPrimaryLang.SelectedValue = DB.tblLanguages.Single(p => p.MYCONLOCID == PLID && p.TenentID == TID).MYCONLOCID.ToString();
                            txtWebsite.Text = ListTBLCOMPANYSETUP[0].WEBPAGE;
                            if (ListTBLCOMPANYSETUP[0].CompanyType != null && Convert.ToInt32(ListTBLCOMPANYSETUP[0].CompanyType) != 0)
                            {
                                int Refid = Convert.ToInt32(ListTBLCOMPANYSETUP[0].CompanyType);
                                List<Database.REFTABLE> CType = DB.REFTABLEs.Where(p => p.TenentID == TID && p.REFID == Refid && p.REFTYPE == "COMP" && p.REFSUBTYPE == "COMPTYPE" && p.ACTIVE == "Y").ToList();
                                if (CType.Count() > 0)
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
                            txtrefreshno.Text = ListTBLCOMPANYSETUP[0].Marketting;
                            txtcUserName.Text = ListTBLCOMPANYSETUP[0].CUSERID;
                            txtcPassword.Text = ListTBLCOMPANYSETUP[0].CPASSWRD;
                            string COID = ListTBLCOMPANYSETUP[0].COUNTRYID.ToString();
                            //  int COID = Convert.ToInt32(ListTBLCOMPANYSETUP[0].COUNTRYID);
                            drpCountry.SelectedValue = COID;
                            bindSates(COID);
                            if (ListTBLCOMPANYSETUP[0].STATE != null)
                            {
                                int COUNTRYID = Convert.ToInt32(COID);
                                int statt = Convert.ToInt32(ListTBLCOMPANYSETUP[0].STATE);
                                List<Database.tblState> STLIST = DB.tblStates.Where(p => p.COUNTRYID == COUNTRYID && p.StateID == statt).ToList();
                                if (STLIST.Count() > 0)
                                {
                                    drpSates.SelectedValue = ListTBLCOMPANYSETUP[0].STATE;
                                }
                            }

                            redonlyture();
                            Button1.Visible = false;
                            btnSubmit.Visible = true;
                            btnSubmit.Text = "Save";
                        }
                    }


                }
                ListCRMActivity();
                ListCRMActivityAll();
                CheckCRMLead();
            }

            //for localization
            Session["Pagename"] = "Suppliers";
            if (!string.IsNullOrEmpty(Session["Language"] as string))
            {
                if (Session["Language"].ToString().StartsWith("ar-KW") == true)
                {
                    b.Attributes.Remove("dir");
                    b.Attributes.Add("dir", "rtl");
                    GetShow();
                }
                else
                {
                    b.Attributes.Remove("dir");
                    b.Attributes.Add("dir", "ltr");

                    GetHide();
                }
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

        public void firstdata()
        {
            if (Listview1.Items.Count > 0)
            {
                Listview1.SelectedIndex = 0;
                if (Listview1.SelectedIndex == 0)
                {
                    int LIDID = Convert.ToInt32(Listview1.SelectedDataKey[0]);
                    //int LIDID = DB.TBLCOMPANYSETUPs.Where(p => p.TenentID == TID).FirstOrDefault().COMPID;
                    //Listview1.SelectedIndex = LIDID;
                    BindEditcompniy(LIDID);
                    string Cname = GetCustomerName(LIDID);
                    lblBusContactDe.Text = "Business Company Details -  (View Mode) - " + Cname;
                    lblBCDeta.Text = "Business Company Details -  (View Mode)- " + Cname;
                    lblwExistance.Text = "Web Existance -  (View Mode)- " + Cname;
                    lblWEmp.Text = "Working Employees -  (View Mode)- " + Cname;

                    redonlyfalse();
                }
            }

        }
        public void GetShow()
        {
            l1.Attributes["class"] = "control-label col-md-2  getshow";
            l11.Attributes["class"] = "control-label col-md-2  gethide";
            lbl12.Attributes["class"] = "control-label col-md-4  getshow";
            lbl13.Attributes["class"] = "control-label col-md-4  gethide";
            lbl1.Attributes["class"] = "control-label col-md-2  gethide";
            lbl2.Attributes["class"] = "control-label col-md-2  getshow";
            lbl3.Attributes["class"] = "control-label col-md-2  gethide";
            Label10.Attributes["class"] = "control-label col-md-2  getshow";
            lbl21.Attributes["class"] = "control-label col-md-4  gethide";
            Label24.Attributes["class"] = "control-label col-md-4  getshow";
            lbl6.Attributes["class"] = "control-label col-md-4  gethide";
            Label26.Attributes["class"] = "control-label col-md-4  getshow";
            lbl7.Attributes["class"] = "control-label col-md-4  gethide";
            Label28.Attributes["class"] = "control-label col-md-4  getshow";
            lbl9.Attributes["class"] = "control-label col-md-4  gethide";
            Label30.Attributes["class"] = "control-label col-md-4  getshow";
            lbl14.Attributes["class"] = "control-label col-md-4  gethide";
            Label32.Attributes["class"] = "control-label col-md-4  getshow";
            lbl87.Attributes["class"] = "control-label col-md-4  gethide";
            Label34.Attributes["class"] = "control-label col-md-4  getshow";
            lbl74.Attributes["class"] = "control-label col-md-4  gethide";
            Label36.Attributes["class"] = "control-label col-md-4  getshow";
            lbl22.Attributes["class"] = "control-label col-md-2  gethide";
            Label38.Attributes["class"] = "control-label col-md-2  getshow";
            lbl55.Attributes["class"] = "control-label col-md-2  gethide";
            Label40.Attributes["class"] = "control-label col-md-2  getshow";
            lbl65.Attributes["class"] = "control-label col-md-2  gethide";
            Label42.Attributes["class"] = "control-label col-md-2  getshow";
            lbl75.Attributes["class"] = "control-label col-md-2  gethide";
            Label44.Attributes["class"] = "control-label col-md-2  getshow";
            lbl78.Attributes["class"] = "control-label col-md-2  gethide";
            Label46.Attributes["class"] = "control-label col-md-2  getshow";
            lbl79.Attributes["class"] = "control-label col-md-2  gethide";
            Label48.Attributes["class"] = "control-label col-md-2  getshow";
            lbl70.Attributes["class"] = "control-label col-md-2  gethide";
            Label51.Attributes["class"] = "control-label col-md-2  getshow";
            lbl121.Attributes["class"] = "control-label col-md-2  gethide";
            Label52.Attributes["class"] = "control-label col-md-2  getshow";
            lbl456.Attributes["class"] = "control-label col-md-2  gethide";
            Label54.Attributes["class"] = "control-label col-md-2  getshow";
            lbl147.Attributes["class"] = "control-label col-md-4  gethide";
            Label56.Attributes["class"] = "control-label col-md-4  getshow";
            // lbl564.Attributes["class"] = "control-label col-md-4  gethide";
            // Label58.Attributes["class"] = "control-label col-md-4  getshow";
            lbl741.Attributes["class"] = "control-label col-md-4  gethide";
            Label60.Attributes["class"] = "control-label col-md-4  getshow";
            //lbl562.Attributes["class"] = "control-label col-md-4  gethide";
            //Label62.Attributes["class"] = "control-label col-md-4  getshow";
            lblrem123.Attributes["class"] = "control-label col-md-2  gethide";
            Label64.Attributes["class"] = "control-label col-md-2  getshow";
            lbl72.Attributes["class"] = "control-label col-md-4  gethide";
            Label66.Attributes["class"] = "control-label col-md-4  getshow";
            lbl521.Attributes["class"] = "control-label col-md-4  gethide";
            Label68.Attributes["class"] = "control-label col-md-4  getshow";
            lbl7521.Attributes["class"] = "control-label col-md-4  gethide";
            Label70.Attributes["class"] = "control-label col-md-4  getshow";
            lbl632.Attributes["class"] = "control-label col-md-4  gethide";
            Label72.Attributes["class"] = "control-label col-md-4  getshow";
            lbl5214.Attributes["class"] = "control-label col-md-4  gethide";
            Label74.Attributes["class"] = "control-label col-md-4  getshow";
        }
        public void GetHide()
        {
            Label74.Attributes["class"] = "control-label col-md-4  gethide";
            lbl5214.Attributes["class"] = "control-label col-md-4  getshow";
            Label72.Attributes["class"] = "control-label col-md-4  gethide";
            lbl632.Attributes["class"] = "control-label col-md-4  getshow";
            Label70.Attributes["class"] = "control-label col-md-4  gethide";
            lbl7521.Attributes["class"] = "control-label col-md-4  getshow";
            Label68.Attributes["class"] = "control-label col-md-4  gethide";
            lbl521.Attributes["class"] = "control-label col-md-4  getshow";
            Label66.Attributes["class"] = "control-label col-md-4  gethide";
            lbl72.Attributes["class"] = "control-label col-md-4  getshow";
            Label64.Attributes["class"] = "control-label col-md-2  gethide";
            lblrem123.Attributes["class"] = "control-label col-md-2  getshow";
            //Label62.Attributes["class"] = "control-label col-md-4  gethide";
            //lbl562.Attributes["class"] = "control-label col-md-4  getshow";
            Label60.Attributes["class"] = "control-label col-md-4  gethide";
            lbl741.Attributes["class"] = "control-label col-md-4  getshow";
            //Label58.Attributes["class"] = "control-label col-md-4  gethide";
            //lbl564.Attributes["class"] = "control-label col-md-4  getshow";
            Label56.Attributes["class"] = "control-label col-md-4  gethide";
            lbl147.Attributes["class"] = "control-label col-md-4  getshow";
            Label54.Attributes["class"] = "control-label col-md-2  gethide";
            lbl456.Attributes["class"] = "control-label col-md-2  getshow";
            Label52.Attributes["class"] = "control-label col-md-2  gethide";
            lbl121.Attributes["class"] = "control-label col-md-2  getshow";
            Label51.Attributes["class"] = "control-label col-md-2  gethide";
            lbl70.Attributes["class"] = "control-label col-md-2  getshow";
            Label48.Attributes["class"] = "control-label col-md-2  gethide";
            lbl79.Attributes["class"] = "control-label col-md-2  getshow";
            Label46.Attributes["class"] = "control-label col-md-2  gethide";
            lbl78.Attributes["class"] = "control-label col-md-2  getshow";
            Label44.Attributes["class"] = "control-label col-md-2  gethide";
            lbl75.Attributes["class"] = "control-label col-md-2  getshow";
            Label42.Attributes["class"] = "control-label col-md-2  gethide";
            lbl65.Attributes["class"] = "control-label col-md-2  getshow";
            Label40.Attributes["class"] = "control-label col-md-2  gethide";
            lbl55.Attributes["class"] = "control-label col-md-2  getshow";
            Label38.Attributes["class"] = "control-label col-md-2  gethide";
            lbl22.Attributes["class"] = "control-label col-md-2  getshow";
            Label36.Attributes["class"] = "control-label col-md-4  gethide";
            lbl74.Attributes["class"] = "control-label col-md-4  getshow";
            Label34.Attributes["class"] = "control-label col-md-4  gethide";
            lbl87.Attributes["class"] = "control-label col-md-4  getshow";
            Label32.Attributes["class"] = "control-label col-md-4  gethide";
            lbl14.Attributes["class"] = "control-label col-md-4  getshow";
            Label30.Attributes["class"] = "control-label col-md-4  gethide";
            lbl9.Attributes["class"] = "control-label col-md-4  getshow";
            Label28.Attributes["class"] = "control-label col-md-4  gethide";
            lbl7.Attributes["class"] = "control-label col-md-4  getshow";
            l1.Attributes["class"] = "control-label col-md-2  gethide";
            l11.Attributes["class"] = "control-label col-md-2  getshow";
            lbl12.Attributes["class"] = "control-label col-md-4  gethide";
            lbl13.Attributes["class"] = "control-label col-md-4  getshow";
            lbl1.Attributes["class"] = "control-label col-md-2  getshow";
            lbl3.Attributes["class"] = "control-label col-md-2  getshow";
            lbl2.Attributes["class"] = "control-label col-md-2  gethide";
            Label10.Attributes["class"] = "control-label col-md-2  gethide";
            lbl21.Attributes["class"] = "control-label col-md-4  getshow";
            Label24.Attributes["class"] = "control-label col-md-4  gethide";
            lbl6.Attributes["class"] = "control-label col-md-4  getshow";
            Label26.Attributes["class"] = "control-label col-md-4  gethide";
        }
        public void redonlyfalse()
        {
            txtAddress.Enabled = false;
            txtcompneySerch.Enabled = false;
            LinkButton12.Enabled = false;
            txtAddress2.Enabled = txtcPassword.Enabled = txtcUserName.Enabled = txtCustomer.Enabled = txtCustomer2.Enabled = txtCustomerName.Enabled = txtMobileNo.Enabled = txtPostalCode.Enabled = txtRemark.Enabled = txtSocial.Enabled = txtWebsite.Enabled = txtZipCode.Enabled = drpcity.Enabled = txtBarCode.Enabled = false;// txtCity.Enabled =
            drpCategory.Enabled = drpbrand.Enabled = drplocation.Enabled = drpCompnay.Enabled = drpCountry.Enabled = drpItManager.Enabled = drpMyProductId.Enabled = drpPrimaryLang.Enabled = drpSomib.Enabled = drpSates.Enabled = drpType.Enabled = drpdatasource.Enabled = drpmarketing.Enabled = false;
            lblBusContactDe.Text = "Business Company Details -  (Read Mode)";
            lblBCDeta.Text = "Business Company Details -  (Read Mode)";
            lblwExistance.Text = "Web Existance -  (Read Mode)";
            lblWEmp.Text = "Working Employees -  (Read Mode)";
            ViewState["ModeData"] = "Read";
            lkbCustomerN1.Enabled = lkbCustomerN2.Enabled = lkbcompnyN3.Enabled = lbkEmail.Enabled = lkbFax.Enabled = lbkBusPhone.Enabled = lkbcheck.Enabled = libtnNewClass.Enabled = LinkButton6.Enabled = LinkButton14.Enabled = LinkButton4.Enabled = btnsocial.Enabled = LinkButton13.Enabled = false;
            txtBirthdate.Enabled = txtCivilID.Enabled = txtPACI.Enabled = false;

        }
        public void redonlyture()
        {
            txtAddress.Enabled = true;
            txtAddress2.Enabled = txtcPassword.Enabled = txtcUserName.Enabled = txtCustomer.Enabled = txtCustomer2.Enabled = txtCustomerName.Enabled = txtMobileNo.Enabled = txtPostalCode.Enabled = txtRemark.Enabled = txtSocial.Enabled = txtWebsite.Enabled = txtZipCode.Enabled = drpcity.Enabled = true;//txtCity.Enabled = 
            drpCategory.Enabled = drpbrand.Enabled = drpCompnay.Enabled = drplocation.Enabled = drpCountry.Enabled = drpItManager.Enabled = drpMyProductId.Enabled = drpPrimaryLang.Enabled = drpSomib.Enabled = drpSates.Enabled = drpType.Enabled = drpdatasource.Enabled = drpmarketing.Enabled = true;
            txtcompneySerch.Enabled = true;
            LinkButton12.Enabled = true;
            lblBusContactDe.Text = "Business Company Details - (Write Mode)";
            lblBCDeta.Text = "Business Company Details - (Write Mode)";
            lblwExistance.Text = "Web Existance -  (Write Mode)";
            lblWEmp.Text = "Working Employees - (Write Mode)";
            ViewState["ModeData"] = "Write";
            lkbCustomerN1.Enabled = lkbCustomerN2.Enabled = lkbcompnyN3.Enabled = lbkEmail.Enabled = lkbFax.Enabled = lbkBusPhone.Enabled = lkbcheck.Enabled = libtnNewClass.Enabled = LinkButton6.Enabled = LinkButton14.Enabled = LinkButton4.Enabled = btnsocial.Enabled = LinkButton13.Enabled = true;
            txtBirthdate.Enabled = txtCivilID.Enabled = txtPACI.Enabled = true;
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
            int Showdata = 100;//Convert.ToInt32(drpShowGrid.SelectedValue);
            int Totalrec = DB.TBLCOMPANYSETUPs.Where(p => p.TenentID == TID).Count();

            if (Totalrec != 0)
            {
                Loadlist(Showdata, take, Skip, ChoiceID, lblShowinfEntry, btnPrevious1, btnNext1, Listview1, ListView2, Totalrec, DB.TBLCOMPANYSETUPs.Where(p => p.TenentID == TID).Take(1).ToList());
            }
        }
        //for Navigation
        public void Loadlist<T>(int Showdata, int take, int Skip, int ChoiceID, Label lblShowinfEntry, LinkButton btnPrevious, LinkButton btnNext, ListView Listview1, ListView ListView3, int Totalrec, List<T> List)
        {
            ViewState["SearchView"] = null;
            List<Database.TBLCOMPANYSETUP> Titel = new List<Database.TBLCOMPANYSETUP>();
            if (ViewState["Title"] != null)
            {
                Titel = ((List<Database.TBLCOMPANYSETUP>)ViewState["Title"]).ToList();
            }

            btnPrevious.Enabled = false;
            if (ViewState["Take"] != null && ViewState["Skip"] != null)
            {
                if (Convert.ToInt32(DrpTitle.SelectedValue) != 999999)
                {

                    if (chkactive.Checked == true)
                    {
                        if (drpSort.SelectedValue == "0")
                        {
                            BindList((Titel.Take(take).Skip(Skip)).ToList());
                        }
                        else if (drpSort.SelectedValue == "1")
                        {
                            BindList((Titel.Take(take).Skip(Skip)).ToList());
                        }
                    }
                    else
                    {
                        if (drpSort.SelectedValue == "0")
                        {
                            BindList((Titel.Take(take).Skip(Skip)).ToList());
                        }
                        else if (drpSort.SelectedValue == "1")
                        {
                            BindList((Titel.Take(take).Skip(Skip)).ToList());
                        }
                    }
                }
                else
                {
                    if (chkactive.Checked == true)
                    {
                        if (drpSort.SelectedValue == "0")
                        {
                            BindList((DB.TBLCOMPANYSETUPs.Where(p => p.TenentID == TID && p.Active == "Y").OrderByDescending(a => a.COMPID).Take(take).Skip(Skip)).ToList());
                        }
                        else if (drpSort.SelectedValue == "1")
                        {
                            BindList((DB.TBLCOMPANYSETUPs.Where(p => p.TenentID == TID && p.Active == "Y").OrderByDescending(a => a.COMPID).Take(take).Skip(Skip)).ToList());
                        }
                    }
                    else
                    {
                        if (drpSort.SelectedValue == "0")
                        {
                            BindList((DB.TBLCOMPANYSETUPs.Where(p => p.TenentID == TID).OrderByDescending(a => a.COMPID).Take(take).Skip(Skip)).ToList());
                        }
                        else if (drpSort.SelectedValue == "1")
                        {
                            BindList((DB.TBLCOMPANYSETUPs.Where(p => p.TenentID == TID).OrderByDescending(a => a.COMPID).Take(take).Skip(Skip)).ToList());
                        }
                    }
                }
                ViewState["Take"] = take;
                ViewState["Skip"] = Skip;
            }
            else
            {
                if (Convert.ToInt32(DrpTitle.SelectedValue) != 999999)
                {
                    if (chkactive.Checked == true)
                    {
                        if (drpSort.SelectedValue == "0")
                        {
                            BindList(Titel.Take(Showdata).Skip(0).ToList());
                        }
                        else if (drpSort.SelectedValue == "1")
                        {
                            BindList(Titel.Take(Showdata).Skip(0).ToList());
                        }
                    }
                    else
                    {
                        if (drpSort.SelectedValue == "0")
                        {
                            BindList(Titel.Take(Showdata).Skip(0).ToList());
                        }
                        else if (drpSort.SelectedValue == "1")
                        {
                            BindList(Titel.Take(Showdata).Skip(0).ToList());
                        }
                    }
                }
                else
                {
                    if (chkactive.Checked == true)
                    {
                        if (drpSort.SelectedValue == "0")
                        {
                            BindList(DB.TBLCOMPANYSETUPs.Where(p => p.TenentID == TID && p.Active == "Y").OrderByDescending(a => a.COMPID).Take(Showdata).Skip(0).ToList());
                        }
                        else if (drpSort.SelectedValue == "1")
                        {
                            BindList(DB.TBLCOMPANYSETUPs.Where(p => p.TenentID == TID && p.Active == "Y").OrderByDescending(a => a.COMPID).Take(Showdata).Skip(0).ToList());
                        }
                    }
                    else
                    {
                        if (drpSort.SelectedValue == "0")
                        {
                            BindList(DB.TBLCOMPANYSETUPs.Where(p => p.TenentID == TID).OrderByDescending(a => a.COMPID).Take(Showdata).Skip(0).ToList());
                        }
                        else if (drpSort.SelectedValue == "1")
                        {
                            BindList(DB.TBLCOMPANYSETUPs.Where(p => p.TenentID == TID).OrderByDescending(a => a.COMPID).Take(Showdata).Skip(0).ToList());
                        }
                    }
                }
                ViewState["Take"] = Showdata;
                ViewState["Skip"] = 0;
            }
            ChoiceList.Clear();
            // int ChoiceID=
            for (int i = 0; i < 10; i++)
            {
                ChoiceID++;
                Navigation Obj = new Navigation();
                Obj.Choice = "";
                Obj.ID = ChoiceID;
                ChoiceList.Add(Obj);
            }
            ViewState["NDATA"] = ChoiceList;
            Navigation();

            if (ViewState["Take"] != null && ViewState["Skip"] != null)
            {
                take = Showdata;
                Skip = 0;
                if (Convert.ToInt32(DrpTitle.SelectedValue) != 999999)
                {
                    if (chkactive.Checked == true)
                    {
                        if (drpSort.SelectedValue == "0")
                        {
                            BindList((Titel.Take(take).Skip(Skip)).ToList());
                        }
                        else if (drpSort.SelectedValue == "1")
                        {
                            BindList((Titel.Take(take).Skip(Skip)).ToList());
                        }

                    }
                    else
                    {
                        if (drpSort.SelectedValue == "0")
                        {
                            BindList((Titel.Take(take).Skip(Skip)).ToList());
                        }
                        else if (drpSort.SelectedValue == "1")
                        {
                            BindList((Titel.Take(take).Skip(Skip)).ToList());
                        }
                    }
                }
                else
                {
                    if (chkactive.Checked == true)
                    {
                        if (drpSort.SelectedValue == "0")
                        {
                            BindList((DB.TBLCOMPANYSETUPs.Where(p => p.TenentID == TID && p.Active == "Y").OrderByDescending(a => a.COMPID).Take(take).Skip(Skip)).ToList());
                        }
                        else if (drpSort.SelectedValue == "1")
                        {
                            BindList((DB.TBLCOMPANYSETUPs.Where(p => p.TenentID == TID && p.Active == "Y").OrderByDescending(a => a.COMPID).Take(take).Skip(Skip)).ToList());
                        }

                    }
                    else
                    {
                        if (drpSort.SelectedValue == "0")
                        {
                            BindList((DB.TBLCOMPANYSETUPs.Where(p => p.TenentID == TID).OrderByDescending(a => a.COMPID).Take(take).Skip(Skip)).ToList());
                        }
                        else if (drpSort.SelectedValue == "1")
                        {
                            BindList((DB.TBLCOMPANYSETUPs.Where(p => p.TenentID == TID).OrderByDescending(a => a.COMPID).Take(take).Skip(Skip)).ToList());
                        }
                    }
                }
                ViewState["Take"] = take;
                ViewState["Skip"] = Skip;
                btnPrevious.Enabled = false;
                ChoiceID = 0;
                if (Convert.ToInt32(DrpTitle.SelectedValue) != 999999)
                {
                    Totalrec = Titel.Count();
                }
                GetCurrentNavigation(ChoiceID, Showdata, ListView2, Totalrec);
                if (take == Totalrec && Skip == (Totalrec - Showdata))
                    btnNext.Enabled = false;
                else
                    btnNext.Enabled = true;

            }
            lblShowinfEntry.Text = "Showing " + Skip + " to " + take + " of " + Totalrec + " entries";
        }
        //For view Navigation

        public void BindListView(List<Database.View_CompanyMaster> List)
        {

            Listview1.DataSource = List;
            Listview1.DataBind();

        }
        public void LoadlistView(int Showdata, int take, int Skip, int ChoiceID, Label lblShowinfEntry, LinkButton btnPrevious, LinkButton btnNext, ListView Listview1, ListView ListView3, int Totalrec, List<Database.View_CompanyMaster> VList)
        {
            //List<Database.View_CompanyMaster> ViewList = ((List<Database.View_CompanyMaster>)ViewState["SearchView"]).ToList();

            btnPrevious.Enabled = false;
            if (ViewState["Take"] != null && ViewState["Skip"] != null)
            {
                BindListView((VList.Take(take).Skip(Skip)).ToList());
                ViewState["Take"] = take;
                ViewState["Skip"] = Skip;
            }
            else
            {
                BindListView(VList.Take(Showdata).Skip(0).ToList());
                ViewState["Take"] = Showdata;
                ViewState["Skip"] = 0;
            }
            ChoiceList.Clear();
            // int ChoiceID=
            for (int i = 0; i < 10; i++)
            {
                ChoiceID++;
                Navigation Obj = new Navigation();
                Obj.Choice = "";
                Obj.ID = ChoiceID;
                ChoiceList.Add(Obj);
            }
            ViewState["NDATA"] = ChoiceList;
            Navigation();

            if (ViewState["Take"] != null && ViewState["Skip"] != null)
            {
                take = Showdata;
                Skip = 0;
                BindListView((VList.Take(take).Skip(Skip)).ToList());
                ViewState["Take"] = take;
                ViewState["Skip"] = Skip;
                btnPrevious.Enabled = false;
                ChoiceID = 0;
                GetCurrentNavigation(ChoiceID, Showdata, ListView2, Totalrec);
                if (take == Totalrec && Skip == (Totalrec - Showdata))
                    btnNext.Enabled = false;
                else
                    btnNext.Enabled = true;

            }
            lblShowinfEntry.Text = "Showing " + Skip + " to " + take + " of " + Totalrec + " entries";
        }

        #region Step5 Navigation
        public void Navigation()
        {
            //Navigati0n
            ListView2.Items.Clear();
            if (ViewState["NDATA"] != null)
            {
                ChoiceList = (List<Navigation>)ViewState["NDATA"];
            }
            else
            {
                ChoiceList = new List<Navigation>();
            }
            ListView2.DataSource = ChoiceList;
            ListView2.DataBind();
        }
        #endregion
        #region Step6 GetCurrentNavigation
        public void GetCurrentNavigation(int ChoiceID, int Showdata, ListView ListView3, int Totalrec)
        {
            int lastchoise = 0;
            ChoiceID = ChoiceID - 5;
            if (ChoiceID <= 0)
            {
                ChoiceID = 0;
            }
            ChoiceList.Clear();
            for (int i = 0; i < 10; i++)
            {
                ChoiceID++;
                lastchoise = Math.Abs(Totalrec / Showdata) + 2;
                if (lastchoise == ChoiceID)
                    break;
                Navigation Obj = new Navigation();
                Obj.Choice = "";
                Obj.ID = ChoiceID;
                ChoiceList.Add(Obj);
            }
            ViewState["NDATA"] = ChoiceList;
            Navigation();
        }
        public void GetCurrentNavigationLast(int ChoiceID, int Showdata, ListView ListView3, int Totalrec)
        {
            int lastchoise = 0;
            ChoiceID = ChoiceID - Showdata;
            if (ChoiceID <= 0)
            {
                ChoiceID = 0;
            }
            ChoiceList.Clear();
            for (int i = 0; i < 5; i++)
            {
                ChoiceID++;
                lastchoise = Math.Abs(Totalrec / Showdata) + 2;
                if (lastchoise == ChoiceID)
                    break;
                Navigation Obj = new Navigation();
                Obj.Choice = "";
                Obj.ID = ChoiceID;
                ChoiceList.Add(Obj);
            }
            ViewState["NDATA"] = ChoiceList;
            Navigation();
        }
        #endregion
        public void BindList(List<Database.TBLCOMPANYSETUP> List)
        {

            Listview1.DataSource = List;
            Listview1.DataBind();

        }

        public void BindDataActive()
        {
            int Countt = DB.TBLCOMPANYSETUPs.Where(p => p.TenentID == TID).Count();
            if (ViewState["SearchView"] != null)
            {
                List<Database.View_CompanyMaster> List = ((List<Database.View_CompanyMaster>)ViewState["SearchView"]).ToList();
                List = List.Where(p => p.Active == "Y").ToList();
                int Showdata = 100;//Convert.ToInt32(drpShowGrid.SelectedValue);
                int Totalrec = List.Count();

                ViewState["SearchView"] = List;
                LoadlistView(Showdata, take, Skip, ChoiceID, lblShowinfEntry, btnPrevious1, btnNext1, Listview1, ListView2, Totalrec, List);
            }
            else
            {
                BindData();
            }
        }


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
            lblcountserch.Text = txtcompneySerch.Text = lblCustomerName.Text = lblCustomerL1.Text = lblCustomerL2.Text = lblEmail12.Text = Label21.Text = lblMobileNo.Text = txtAddress.Text = txtAddress2.Text = txtcPassword.Text = txtcUserName.Text = txtCustomer.Text = txtCustomer2.Text = txtCustomerName.Text = txtMobileNo.Text = txtPostalCode.Text = tags_5.Text = txtRemark.Text = txtWebsite.Text = txtZipCode.Text = tags_1.Text = txtrefreshno.Text = tags_2.Text = tags_3.Text = tags_4.Text = txtBarCode.Text = "";//txtCity.Text = 
            drpCategory.SelectedIndex = drpcity.SelectedIndex = drpmarketing.SelectedIndex = 0;
            chbIsMinistry.Checked = chbIssMb.Checked = chbIsCorporate.Checked = chbInHawally.Checked = chbSaler.Checked = chbBuyer.Checked = chbSaleDeProd.Checked = chbEmailSub.Checked = false;
            drpSates.SelectedIndex = 0;
            drpItManager.SelectedIndex = 0;
            //drpMyCounLocID.SelectedIndex = 0;
            drpMyProductId.SelectedIndex = 0;
            drpPrimaryLang.SelectedIndex = 0;
            //drpState.SelectedIndex = 0;
            drpType.SelectedIndex = 0;
            drpdatasource.SelectedIndex = 0;
            drpCountry.SelectedIndex = 0;
            txtBirthdate.Text = txtCivilID.Text = "";

        }
        protected void ListCustomerMaster_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (e.CommandName == "btnActive")
            {
                int ID = Convert.ToInt32(e.CommandArgument);
                if (DB.TBLCOMPANYSETUPs.Where(p => p.COMPID == ID && p.TenentID == TID && p.Active == "Y").Count() > 0)
                {
                    if (DB.tblcontact_addon1.Where(p => p.TenentID == TID && p.CustomerId == ID && p.active == false).Count() < 1)
                    {
                        Database.TBLCOMPANYSETUP objtbl_COMPANYSETUP = DB.TBLCOMPANYSETUPs.Single(p => p.COMPID == ID && p.TenentID == TID && p.Active == "Y");
                        objtbl_COMPANYSETUP.Active = "N";
                        DB.SaveChanges();

                        String url = "Active TBLCOMPANYSETUP with " + "TenentID = " + TID + "CustomerId =" + ID;
                        String evantname = "Active";
                        String tablename = "TBLCOMPANYSETUP";
                        string loginUserId = (((USER_MST)HttpContext.Current.Session["USER"]).USER_ID).ToString();

                        Classes.GlobleClass.EncryptionHelpers.WriteLog(url, evantname, tablename, loginUserId.ToString(), 0,0);

                    }
                    else
                    {
                        Classes.Toastr.ShowToast(Page, Classes.Toastr.ToastType.Error, "Customer can not DeActive It Used In Subscriber Setup", "Error!", Classes.Toastr.ToastPosition.TopCenter);
                    }
                }
                else
                {
                    Database.TBLCOMPANYSETUP objtbl_COMPANYSETUP = DB.TBLCOMPANYSETUPs.Single(p => p.COMPID == ID && p.TenentID == TID);
                    objtbl_COMPANYSETUP.Active = "Y";
                    DB.SaveChanges();

                    String url = "Active TBLCOMPANYSETUP with " + "TenentID = " + TID + "COMPID =" + ID;
                    String evantname = "Active";
                    String tablename = "TBLCOMPANYSETUP";
                    string loginUserId = (((USER_MST)HttpContext.Current.Session["USER"]).USER_ID).ToString();

                    Classes.GlobleClass.EncryptionHelpers.WriteLog(url, evantname, tablename, loginUserId.ToString(), 0,0);
                }
                BindData();
            }
            if (e.CommandName == "btnDelete")
            {
                int ID = Convert.ToInt32(e.CommandArgument);
                if (DB.tblcontact_addon1.Where(p => p.TenentID == TID && p.CustomerId == ID).Count() < 1)
                {
                    if (DB.TBLCOMPANYSETUPs.Where(p => p.COMPID == ID && p.TenentID == TID && p.Active == "Y").Count() > 0)
                    {
                        Database.TBLCOMPANYSETUP objtbl_COMPANYSETUP = DB.TBLCOMPANYSETUPs.Single(p => p.COMPID == ID && p.TenentID == TID && p.Active == "Y");
                        objtbl_COMPANYSETUP.Active = "N";
                        //objtbl_COMPANYSETUP.Approved = 1;
                        DB.SaveChanges();

                        String url = "Active TBLCOMPANYSETUP with " + "TenentID = " + TID + "COMPID =" + ID;
                        String evantname = "Active";
                        String tablename = "TBLCOMPANYSETUP";
                        string loginUserId = (((USER_MST)HttpContext.Current.Session["USER"]).USER_ID).ToString();

                        Classes.GlobleClass.EncryptionHelpers.WriteLog(url, evantname, tablename, loginUserId.ToString(), 0,0);

                        BindData();
                        Classes.Toastr.ShowToast(Page, Classes.Toastr.ToastType.Success, "Data Delete Successfully", "Success!", Classes.Toastr.ToastPosition.TopCenter);
                    }
                    else
                    {

                    }
                }
                else
                {
                    Classes.Toastr.ShowToast(Page, Classes.Toastr.ToastType.Error, "Customer can not Delete It Used In Subscriber Setup", "Error!", Classes.Toastr.ToastPosition.TopCenter);
                }

            }
            if (e.CommandName == "btnview")
            {
                int COMPID = Convert.ToInt32(Request.QueryString["COMPID"]);
                txtCustoID.Text = COMPID.ToString();
                txtCustoID.Enabled = false;
                BindEditcompniy(COMPID);
                string Cname = GetCustomerName(COMPID);
                Session["ADMInPrevious"] = Session["ADMInCurrent"];
                Session["ADMInCurrent"] = Request.RawUrl;
                redonlyfalse();
                lblBusContactDe.Text = "Business Company Details -  (View Mode) - " + Cname;
                lblBCDeta.Text = "Business Company Details -  (View Mode)- " + Cname;
                lblwExistance.Text = "Web Existance -  (View Mode)- " + Cname;
                lblWEmp.Text = "Working Employees -  (View Mode)- " + Cname;
                btnattmentmst.Visible = true;
                PanalFBImage.Visible = true;
                lblAttecmentcount.Text = Countatt(COMPID);
                btnSubmit.Visible = false;
                Button1.Visible = true;
            }
            if (e.CommandName == "btnEdit")
            {
                int COMPID = Convert.ToInt32(Request.QueryString["COMPID"]);
                txtCustoID.Text = COMPID.ToString();
                txtCustoID.Enabled = false;
                //LinkButton13.Visible = true;
                BindEditcompniy(COMPID);
                string Cname = GetCustomerName(COMPID);
                Session["ADMInPrevious"] = Session["ADMInCurrent"];
                Session["ADMInCurrent"] = Request.RawUrl;
                redonlyture();
                lblBusContactDe.Text = "Business Company Details -Edit Mode - " + Cname;
                lblBCDeta.Text = "Business Company Details -Edit Mode - " + Cname;
                lblwExistance.Text = "Web Existance -Edit Mode - " + Cname;
                lblWEmp.Text = "Working Employees -Edit Mode - " + Cname;
                btnSubmit.Visible = true;
                Button1.Visible = false;
                btnSubmit.Text = "Update";
                btnattmentmst.Visible = true;
                PanalFBImage.Visible = true;
                lblAttecmentcount.Text = Countatt(COMPID);
                LinkAddEmployee.Attributes["Style"] = "display:block;";
                btnSaveandConti.Text = "Update & continues";
                btnSaveandConti.Visible = true;
            }
            //if (e.CommandName == "btnSelect")
            //{                
            //    List<Database.TBLCOMPANYSETUP_ExcelImport> Tools = new List<Database.TBLCOMPANYSETUP_ExcelImport>();
            //    if (ViewState["Tools"] != null)
            //        Tools = ((List<Database.TBLCOMPANYSETUP_ExcelImport>)ViewState["Tools"]).ToList();
            //    int VID = Convert.ToInt32(e.CommandArgument);

            //    if (Tools.Count() == 0)
            //    {
            //        Database.TBLCOMPANYSETUP_ExcelImport obj1 = new Database.TBLCOMPANYSETUP_ExcelImport();
            //        obj1.TenentID = TID;
            //        obj1.COMPID = VID;
            //        Tools.Add(obj1);
            //        ViewState["Tools"] = Tools;
            //        pnlToolss.Visible = true;
            //    }
            //    else
            //    {
            //       if(Tools.Where(p=>p.COMPID == VID).Count() >= 1)
            //       {

            //       }
            //        else
            //       {
            //           Database.TBLCOMPANYSETUP_ExcelImport obj1 = new Database.TBLCOMPANYSETUP_ExcelImport();
            //           obj1.TenentID = TID;
            //           obj1.COMPID = VID;
            //           Tools.Add(obj1);
            //           ViewState["Tools"] = Tools;
            //           pnlToolss.Visible = true;
            //       }
            //    }


            //}

        }

        public string Countatt(int COMPID)
        {
            var List = DB.tbl_DMSAttachmentMst.Where(p => p.AttachmentById == COMPID && p.AttachmentByName == "CompanyMaster" && p.TenentID == TID).ToList();
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
            txtCustoID.Text = ID.ToString();
            txtCustoID.Enabled = false;
            Classes.EcommAdminClass.getdropdown(drplocation, TID, "1", "", "", "TBLCOMPANY_LOCATION");
            Database.TBLCOMPANYSETUP objtbl_COMPANYSETUP = DB.TBLCOMPANYSETUPs.Single(p => p.TenentID == TID && p.COMPID == ID);

            //drpdatasource.DataSource = DB.TBLCOMPANYSETUPs.Where(p => p.TenentID == TID && p.COMPID != ID).OrderBy(p => p.COMPNAME1);
            //drpdatasource.DataTextField = "COMPNAME1";
            //drpdatasource.DataValueField = "COMPID";
            //drpdatasource.DataBind();
            //drpdatasource.Items.Insert(0, new ListItem("-- Select --", "0"));

            drplocation.SelectedValue = objtbl_COMPANYSETUP.PHYSICALLOCID;
            if (objtbl_COMPANYSETUP.PACI != null)
                txtPACI.Text = objtbl_COMPANYSETUP.PACI.ToString();
            btnSubmit.Text = "Update";
            txtCustomerName.Text = objtbl_COMPANYSETUP.COMPNAME1;
            txtCustomer.Text = objtbl_COMPANYSETUP.COMPNAME2;
            txtCustomer2.Text = objtbl_COMPANYSETUP.COMPNAME3;
            tags_2.Text = objtbl_COMPANYSETUP.EMAIL1 != null && objtbl_COMPANYSETUP.EMAIL1 != "" ? objtbl_COMPANYSETUP.EMAIL1 : "";
            TextBox2.Text = tags_2.Text;
            //if (objtbl_COMPANYSETUP.ITMANAGER != null || objtbl_COMPANYSETUP.ITMANAGER != "0")
            //{
            //    drpItManager.SelectedValue = objtbl_COMPANYSETUP.ITMANAGER;
            //}

            txtAddress.Text = objtbl_COMPANYSETUP.ADDR1 != null && objtbl_COMPANYSETUP.ADDR1 != "" ? objtbl_COMPANYSETUP.ADDR1 : "";
            txtAddress2.Text = objtbl_COMPANYSETUP.ADDR2 != null && objtbl_COMPANYSETUP.ADDR2 != "" ? objtbl_COMPANYSETUP.ADDR2 : "";
            if (objtbl_COMPANYSETUP.BirthDate != null)
            {
                DateTime BirthDate = Convert.ToDateTime(objtbl_COMPANYSETUP.BirthDate);
                txtBirthdate.Text = BirthDate.ToString("MM/dd/yyyy");
            }
            else
            {
                txtBirthdate.Text = "";
            }

            if (objtbl_COMPANYSETUP.datasource != 0 && objtbl_COMPANYSETUP.datasource != null)
            {
                drpdatasource.SelectedValue = objtbl_COMPANYSETUP.datasource.ToString();
            }
            else
            {
                drpdatasource.SelectedValue = "0";
            }

            txtCivilID.Text = objtbl_COMPANYSETUP.CivilID != null && objtbl_COMPANYSETUP.CivilID != "" ? objtbl_COMPANYSETUP.CivilID : "";
            // txtCity.Text = objtbl_COMPANYSETUP.CITY;

            txtPostalCode.Text = objtbl_COMPANYSETUP.POSTALCODE != null && objtbl_COMPANYSETUP.POSTALCODE != "" ? objtbl_COMPANYSETUP.POSTALCODE : "";
            txtZipCode.Text = objtbl_COMPANYSETUP.ZIPCODE != null && objtbl_COMPANYSETUP.ZIPCODE != "" ? objtbl_COMPANYSETUP.ZIPCODE : "";
            //drpMyCounLocID.SelectedValue = objtbl_COMPANYSETUP.MYCONLOCID.ToString();
            if (objtbl_COMPANYSETUP.MYPRODID != null && objtbl_COMPANYSETUP.MYPRODID != 0)
                drpMyProductId.SelectedValue = objtbl_COMPANYSETUP.MYPRODID.ToString();

            tags_4.Text = objtbl_COMPANYSETUP.BUSPHONE1 != null && objtbl_COMPANYSETUP.BUSPHONE1 != "" ? objtbl_COMPANYSETUP.BUSPHONE1 : "";
            txtMobileNo.Text = objtbl_COMPANYSETUP.MOBPHONE != null && objtbl_COMPANYSETUP.MOBPHONE != "" ? objtbl_COMPANYSETUP.MOBPHONE : "";
            TextBox3.Text = txtMobileNo.Text;
            txtBarCode.Text = objtbl_COMPANYSETUP.BARCODE != null && objtbl_COMPANYSETUP.BARCODE != "" ? objtbl_COMPANYSETUP.BARCODE : "";
            if (txtBarCode.Text != "")
            {
                showBarcode(txtBarCode.Text);
            }
            tags_3.Text = objtbl_COMPANYSETUP.FAX != null && objtbl_COMPANYSETUP.FAX != "" ? objtbl_COMPANYSETUP.FAX : "";
            if (objtbl_COMPANYSETUP.PRIMLANGUGE != null && objtbl_COMPANYSETUP.PRIMLANGUGE != "0" && objtbl_COMPANYSETUP.PRIMLANGUGE != "")
            {
                int PLID = Convert.ToInt32(objtbl_COMPANYSETUP.PRIMLANGUGE);
                drpPrimaryLang.SelectedValue = DB.tblLanguages.Single(p => p.MYCONLOCID == PLID && p.TenentID == TID).MYCONLOCID.ToString();
            }

            txtWebsite.Text = objtbl_COMPANYSETUP.WEBPAGE != null && objtbl_COMPANYSETUP.WEBPAGE != "" ? objtbl_COMPANYSETUP.WEBPAGE : "";
            if (objtbl_COMPANYSETUP.CompanyType != null && Convert.ToInt32(objtbl_COMPANYSETUP.CompanyType) != 0)
            {
                int Refid = Convert.ToInt32(objtbl_COMPANYSETUP.CompanyType);
                List<Database.REFTABLE> CType = DB.REFTABLEs.Where(p => p.TenentID == TID && p.REFID == Refid && p.REFTYPE == "COMP" && p.REFSUBTYPE == "COMPTYPE" && p.ACTIVE == "Y").ToList();
                if (CType.Count() > 0)
                    drpType.SelectedValue = (objtbl_COMPANYSETUP.CompanyType).ToString();
            }
            else
            {
                drpType.SelectedValue = "82005".ToString();
            }

            chbIsMinistry.Checked = (objtbl_COMPANYSETUP.ISMINISTRY == true) ? true : false;
            chbIssMb.Checked = (objtbl_COMPANYSETUP.ISSMB == true) ? true : false;
            chbIsCorporate.Checked = (objtbl_COMPANYSETUP.ISCORPORATE == true) ? true : false;
            chbInHawally.Checked = (objtbl_COMPANYSETUP.INHAWALLY == true) ? true : false;
            chbSaler.Checked = (objtbl_COMPANYSETUP.SALER == true) ? true : false;
            chbBuyer.Checked = (objtbl_COMPANYSETUP.BUYER == true) ? true : false;
            chbSaleDeProd.Checked = (objtbl_COMPANYSETUP.SALEDEPROD == true) ? true : false;
            chbEmailSub.Checked = (objtbl_COMPANYSETUP.EMAISUB == true) ? true : false;
            tags_5.Text = objtbl_COMPANYSETUP.PRODUCTDEALIN != null && objtbl_COMPANYSETUP.PRODUCTDEALIN != "" ? objtbl_COMPANYSETUP.PRODUCTDEALIN : "";
            txtRemark.Text = objtbl_COMPANYSETUP.REMARKS;
            tags_1.Text = objtbl_COMPANYSETUP.Keyword != null && objtbl_COMPANYSETUP.Keyword != "" ? objtbl_COMPANYSETUP.Keyword : "";
            txtrefreshno.Text = objtbl_COMPANYSETUP.Marketting != null && objtbl_COMPANYSETUP.Marketting != "" ? objtbl_COMPANYSETUP.Marketting : "";
            txtcUserName.Text = objtbl_COMPANYSETUP.CUSERID != null && objtbl_COMPANYSETUP.CUSERID != "" ? objtbl_COMPANYSETUP.CUSERID : "";
            txtcPassword.Text = objtbl_COMPANYSETUP.CPASSWRD != null && objtbl_COMPANYSETUP.CPASSWRD != "" ? objtbl_COMPANYSETUP.CPASSWRD : "";

            string COID = objtbl_COMPANYSETUP.COUNTRYID.ToString();
            if (COID != null && COID != "0")
            {
                drpCountry.SelectedValue = COID.ToString();
            }
            bindSates(COID);
            if (objtbl_COMPANYSETUP.STATE != null && objtbl_COMPANYSETUP.STATE != "0")
            {
                int COUNTRYID = Convert.ToInt32(COID);
                int statt = Convert.ToInt32(objtbl_COMPANYSETUP.STATE);
                List<Database.tblState> STLIST = DB.tblStates.Where(p => p.COUNTRYID == COUNTRYID && p.StateID == statt).ToList();
                if (STLIST.Count() > 0)
                {
                    drpSates.SelectedValue = objtbl_COMPANYSETUP.STATE;
                }
            }
            int state = Convert.ToInt32(drpSates.SelectedValue);
            bindcity(state);
            if (objtbl_COMPANYSETUP.CITY != null && objtbl_COMPANYSETUP.CITY != "0")
            {
                int COUNTRYID = Convert.ToInt32(COID);
                int city = Convert.ToInt32(objtbl_COMPANYSETUP.CITY);
                List<Database.tblCityStatesCounty> CTLIST = DB.tblCityStatesCounties.Where(p => p.COUNTRYID == COUNTRYID && p.StateID == state && p.CityID == city).ToList();
                if (CTLIST.Count() > 0)
                    drpcity.SelectedValue = objtbl_COMPANYSETUP.CITY;
            }

            if (objtbl_COMPANYSETUP.Avtar != null && objtbl_COMPANYSETUP.Avtar != "0" && objtbl_COMPANYSETUP.Avtar != "")
            {
                Avatar.ImageUrl = "/CRM/Upload/" + objtbl_COMPANYSETUP.Avtar;
            }
            else
            {
                Avatar.ImageUrl = "/CRM/Upload/defolt.png";
            }

            SoiclMediya(ID);
            BindCompanywisecontact(ID);

            int CIDdd = ID;
            List<Database.tbl_DMSAttachmentMst> AttList = DB.tbl_DMSAttachmentMst.Where(p => p.TenentID == TID).ToList();
            if (AttList.Where(p => p.TenentID == TID && p.AttachmentById == CIDdd && p.AttachmentByName == "CompanyMaster" && p.AttachmentType == "111001").Count() == 1)
            {
                Database.tbl_DMSAttachmentMst DMSAttachmentMst = AttList.Single(p => p.TenentID == TID && p.AttachmentById == CIDdd && p.AttachmentByName == "CompanyMaster" && p.AttachmentType == "111001");
                ImageFront.ImageUrl = "../Gallery/" + DMSAttachmentMst.AttachmentPath;
                ImgFront.ImageUrl = "../Gallery/" + DMSAttachmentMst.AttachmentPath;
            }
            else
            {
                ImageFront.ImageUrl = "/CRM/Upload/defolt.png";
                ImgFront.ImageUrl = "/CRM/Upload/defolt.png";
            }
            if (AttList.Where(p => p.TenentID == TID && p.AttachmentById == CIDdd && p.AttachmentByName == "CompanyMaster" && p.AttachmentType == "111002").Count() == 1)
            {
                Database.tbl_DMSAttachmentMst DMSAttachmentMst = AttList.Single(p => p.TenentID == TID && p.AttachmentById == CIDdd && p.AttachmentByName == "CompanyMaster" && p.AttachmentType == "111002");
                ImageBack.ImageUrl = "../Gallery/" + DMSAttachmentMst.AttachmentPath;
                ImgBack.ImageUrl = "../Gallery/" + DMSAttachmentMst.AttachmentPath;
            }
            else
            {
                ImageBack.ImageUrl = "/CRM/Upload/defolt.png";
                ImgBack.ImageUrl = "/CRM/Upload/defolt.png";
            }
            LinkAddEmployee.Attributes["Style"] = "display:none;";
            Bindmemo(ID);
            BindAppoint(ID);
            BindListEmail(ID);
        }

        public void bindcity(int State)
        {
            if (State != 0)
            {
                drpcity.DataSource = DB.tblCityStatesCounties.Where(p => p.StateID == State).OrderBy(p => p.CityEnglish);
                drpcity.DataTextField = "CityEnglish";
                drpcity.DataValueField = "CityID";
                drpcity.DataBind();
                drpcity.Items.Insert(0, new ListItem("-- Select --", "0"));
            }
            else
            {
                drpcity.DataSource = DB.tblCityStatesCounties.OrderBy(p => p.CityEnglish);
                drpcity.DataTextField = "CityEnglish";
                drpcity.DataValueField = "CityID";
                drpcity.DataBind();
                drpcity.Items.Insert(0, new ListItem("-- Select --", "0"));
            }

        }

        public string getFirstCommaseprate(string SeperateVal)
        {
            //if (String.IsNullOrEmpty(SeperateVal))
            //{

            //}
            string[] Seperate = SeperateVal.Split(',');
            string FirstValue = Seperate[0];
            return FirstValue;
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            PanelError.Visible = false;
            lblerror.Text = "";
            if (txtCustoID.Text != "")
            {
                //int CID = Convert.ToInt32(txtCustoID.Text);
                //if (DB.TBLCOMPANYSETUPs.Where(p => p.TenentID == TID && p.COMPID == CID).Count() > 0)
                //{
                //    PanelError.Visible = true;
                //    lblerror.Text = "Customer ID Already Exist";
                //    return;
                //}
            }
            else
            {
                PanelError.Visible = true;
                lblerror.Text = "Please Enter Customer ID";
                return;
            }

            if (txtMobileNo.Text != "")
            {
                string[] Seperate2 = txtMobileNo.Text.Split(',');
                for (int i = 0; i <= Seperate2.Count() - 1; i++)
                {
                    string Sep2 = Seperate2[i];
                    int Length = Sep2.Length;
                    if (Length < 7)
                    {
                        PanelError.Visible = true;
                        lblerror.Text = "Please Enter Valid Mobile No.";
                        return;
                    }
                    string str = Sep2.Substring(0, 2);

                    int Countryid = Convert.ToInt32(drpCountry.SelectedValue);
                    if (DB.tblCOUNTRies.Where(p => p.TenentID == TID && p.COUNTRYID == Countryid && p.TelLength != null && p.TelLength != 0).Count() > 0)
                    {
                        int checklen = Convert.ToInt32(DB.tblCOUNTRies.Single(p => p.TenentID == TID && p.COUNTRYID == Countryid && p.TelLength != null && p.TelLength != 0).TelLength);
                        if (str == "18")
                        {
                            if (Length < 7)
                            {
                                PanelError.Visible = true;
                                lblerror.Text = "Please Enter minimum 7 Digit of TollFree No";
                                return;
                            }
                        }
                        else if (Length == 8)
                        {

                        }
                        else
                        {
                            PanelError.Visible = true;
                            lblerror.Text = "Please Enter minimum 8 Digit of Mobile No";
                            return;
                        }
                    }
                    else
                    {
                        if (str == "18")
                        {
                            if (Length < 7)
                            {
                                PanelError.Visible = true;
                                lblerror.Text = "Please Enter minimum 7 Digit of TollFree No";
                                return;
                            }
                        }
                        else if (Length == 8)
                        {
                            PanelError.Visible = true;
                            lblerror.Text = "Please Enter minimum 8 Digit of Mobile No";
                            return;
                        }
                    }
                }
            }

            if (txtBirthdate.Text != "")
            {
                DateTime bday = Convert.ToDateTime(txtBirthdate.Text);
                DateTime today = DateTime.Now;
                if (bday <= today)
                { }
                else
                {
                    PanelError.Visible = true;
                    lblerror.Text = "Enter Birthday not greater than today";
                    return;
                }
            }


            using (var scope = new System.Transactions.TransactionScope())
            {
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

                    txtCustoID.Text = COMPID.ToString();

                    objtbl_COMPANYSETUP.TenentID = TID;
                    objtbl_COMPANYSETUP.PACI = txtPACI.Text;
                    objtbl_COMPANYSETUP.PHYSICALLOCID = drplocation.SelectedValue;
                    objtbl_COMPANYSETUP.COMPNAME1 = txtCustomerName.Text;
                    objtbl_COMPANYSETUP.COMPNAME2 = txtCustomer.Text;
                    objtbl_COMPANYSETUP.COMPNAME3 = txtCustomer2.Text;
                    objtbl_COMPANYSETUP.EMAIL1 = getFirstCommaseprate(tags_2.Text);
                    // objtbl_COMPANYSETUP.EMAIL2 = tags_2.Text;
                    objtbl_COMPANYSETUP.ITMANAGER = drpItManager.SelectedValue;
                    objtbl_COMPANYSETUP.ADDR1 = txtAddress.Text;
                    objtbl_COMPANYSETUP.ADDR2 = txtAddress2.Text;
                    if (txtBirthdate.Text != "")
                    {
                        objtbl_COMPANYSETUP.BirthDate = Convert.ToDateTime(txtBirthdate.Text);
                    }

                    objtbl_COMPANYSETUP.CivilID = txtCivilID.Text;
                    //objtbl_COMPANYSETUP.CITY = txtCity.Text;
                    objtbl_COMPANYSETUP.CITY = drpcity.SelectedValue;
                    objtbl_COMPANYSETUP.STATE = drpSates.SelectedValue;
                    objtbl_COMPANYSETUP.POSTALCODE = txtPostalCode.Text;
                    objtbl_COMPANYSETUP.ZIPCODE = txtZipCode.Text;
                    //objtbl_COMPANYSETUP.MYCONLOCID = Convert.ToInt32(drpMyCounLocID.SelectedValue);
                    objtbl_COMPANYSETUP.MYPRODID = Convert.ToInt32(drpMyProductId.SelectedValue);
                    //   string CNAME = drpCountry.SelectedItem.ToString();
                    objtbl_COMPANYSETUP.COUNTRYID = Convert.ToInt32(drpCountry.SelectedValue);
                    objtbl_COMPANYSETUP.BUSPHONE1 = getFirstCommaseprate(tags_4.Text);
                    //objtbl_COMPANYSETUP.BUSPHONE2 = tags_4.Text;
                    //objtbl_COMPANYSETUP.BUSPHONE3 = tags_4.Text;
                    //objtbl_COMPANYSETUP.BUSPHONE4 = tags_4.Text;
                    objtbl_COMPANYSETUP.MOBPHONE = getFirstCommaseprate(txtMobileNo.Text);
                    objtbl_COMPANYSETUP.BARCODE = txtBarCode.Text;
                    objtbl_COMPANYSETUP.FAX = getFirstCommaseprate(tags_3.Text);
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
                    objtbl_COMPANYSETUP.Marketting = txtrefreshno.Text;
                    objtbl_COMPANYSETUP.CUSERID = txtcUserName.Text;
                    objtbl_COMPANYSETUP.CPASSWRD = txtcPassword.Text;
                    objtbl_COMPANYSETUP.USERID = UID;
                    if (drpdatasource.SelectedValue != "0")
                        objtbl_COMPANYSETUP.datasource = Convert.ToInt32(drpdatasource.SelectedValue);
                    //objtbl_COMPANYSETUP.ENTRYDATE = DateTime.Now;
                    //objtbl_COMPANYSETUP.ENTRYTIME = DateTime.Now;
                    objtbl_COMPANYSETUP.UPDTTIME = DateTime.Now;
                    objtbl_COMPANYSETUP.Approved = 0;

                    if (avatarUploadd.HasFile)
                    {
                        string path = COMPID + "_" + TID + "_" + txtCustomerName.Text + "_TBLCOMPANYSETUP_" + avatarUploadd.FileName;
                        avatarUploadd.SaveAs(Server.MapPath("/CRM/Upload/" + path));
                        objtbl_COMPANYSETUP.Avtar = path;
                    }
                    if (objtbl_COMPANYSETUP.CRUP_ID != null)
                    {
                        int crupID = Convert.ToInt32(objtbl_COMPANYSETUP.CRUP_ID);
                        GlobleClass.EncryptionHelpers.UpdateLog("CRM_Company_Edit Company(Update):", crupID, "TBLCOMPANYSETUP", UID.ToString());
                    }
                    DB.SaveChanges();

                    String url = "insert new record in TBLCOMPANYSETUP with " + "TenentID = " + TID + "COMPID =" + COMPID;
                    String evantname = "create";
                    String tablename = "TBLCOMPANYSETUP";
                    string loginUserId = (((USER_MST)HttpContext.Current.Session["USER"]).USER_ID).ToString();

                    Classes.GlobleClass.EncryptionHelpers.WriteLog(url, evantname, tablename, loginUserId.ToString(), 0,0);

                    // COMPNY CLASSIFICATION
                    if (tags_1.Text != "")
                    {
                        List<Tbl_RecordType_Mst> List4;
                        if (COMPID != null && COMPID != 0)
                            List4 = DB.Tbl_RecordType_Mst.Where(p => p.CompniyAndContactID == COMPID && p.RecordType == "Classification" && p.Recource == 5005 && p.TenentID == TID).ToList();

                        else
                            List4 = DB.Tbl_RecordType_Mst.Where(p => p.CompniyAndContactID == CID && p.RecordType == "Classification" && p.Recource == 5005 && p.TenentID == TID).ToList();



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
                            obj.Recource = 5005;//REFTABLE.Rafid , reftype="CRM" ,refsubtype="RecordTypeanddisplay"
                            obj.RecourceName = "Company";
                            obj.RecValue = Sep4;
                            obj.Active = true;
                            // obj.Rremark = "AutomatedProcess";
                            DB.Tbl_RecordType_Mst.AddObject(obj);
                            DB.SaveChanges();

                            String url2 = "insert new record in Tbl_RecordType_Mst with " + "TenentID = " + TID + "COMPID =" + COMPID;
                            String evantname3 = "create";
                            String tablename4 = "Tbl_RecordType_Mst";
                            string loginUserId5 = (((USER_MST)HttpContext.Current.Session["USER"]).USER_ID).ToString();

                            Classes.GlobleClass.EncryptionHelpers.WriteLog(url2, evantname3, tablename4, loginUserId5.ToString(), 0,0);

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

                            String url2 = "insert new record in Tbl_RecordType_Mst with " + "TenentID = " + TID + "COMPID =" + COMPID;
                            String evantname3 = "create";
                            String tablename4 = "Tbl_RecordType_Mst";
                            string loginUserId5 = (((USER_MST)HttpContext.Current.Session["USER"]).USER_ID).ToString();

                            Classes.GlobleClass.EncryptionHelpers.WriteLog(url2, evantname3, tablename4, loginUserId5.ToString(), 0,0);
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
                            var exist = DB.Tbl_RecordType_Mst.Where(c => c.RecValue == Sep && c.RecordType == "Email" && c.TenentID == TID && c.Recource == 5005 && c.CompniyAndContactID != COMPID);
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

                                String url2 = "insert new record in Tbl_RecordType_Mst with " + "TenentID = " + TID + "COMPID =" + COMPID;
                                String evantname3 = "create";
                                String tablename4 = "Tbl_RecordType_Mst";
                                string loginUserId5 = (((USER_MST)HttpContext.Current.Session["USER"]).USER_ID).ToString();

                                Classes.GlobleClass.EncryptionHelpers.WriteLog(url2, evantname3, tablename4, loginUserId5.ToString(), 0,0);
                            }

                            else
                            {
                                if (DB.Tbl_RecordType_Mst.Where(c => c.RecValue == Sep && c.RecordType == "Email" && c.TenentID == TID && c.Recource == 5005 && c.CompniyAndContactID != COMPID).Count() < 1)
                                {
                                    if (Sep != "NotUsed@gmail.com")
                                    {
                                        string display = "Email Is Duplicate!";
                                        ClientScript.RegisterStartupScript(this.GetType(), "Email Is Duplicate!", "alert('" + display + "');", true);
                                        return;
                                    }
                                }

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
                            var exist = DB.Tbl_RecordType_Mst.Where(c => c.RecValue == Sep1 && c.RecordType == "Fax" && c.TenentID == TID && c.Recource == 5005 && c.CompniyAndContactID != COMPID);
                            if (exist.Count() <= 0)
                            {
                                int Length = Sep1.Length;
                                int Countryid = Convert.ToInt32(drpCountry.SelectedValue);
                                if (DB.tblCOUNTRies.Where(p => p.TenentID == TID && p.COUNTRYID == Countryid && p.FaxLength != null).Count() > 0)
                                {
                                    int checklen = Convert.ToInt32(DB.tblCOUNTRies.Single(p => p.TenentID == TID && p.COUNTRYID == Countryid && p.FaxLength != null).FaxLength);
                                    if (Length < 8)
                                    {
                                        PanelError.Visible = true;
                                        lblerror.Text = "Please Enter minimum 8 Digit of Fax";
                                        return;
                                    }
                                }
                                else
                                {
                                    if (Length < 8)
                                    {
                                        PanelError.Visible = true;
                                        lblerror.Text = "Please Enter minimum 8 Digit of Fax";
                                        return;
                                    }
                                }

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

                                String url2 = "insert new record in Tbl_RecordType_Mst with " + "TenentID = " + TID + "COMPID =" + COMPID;
                                String evantname3 = "create";
                                String tablename4 = "Tbl_RecordType_Mst";
                                string loginUserId5 = (((USER_MST)HttpContext.Current.Session["USER"]).USER_ID).ToString();

                                Classes.GlobleClass.EncryptionHelpers.WriteLog(url2, evantname3, tablename4, loginUserId5.ToString(), 0,0);
                            }
                            else
                            {
                                if (exist.Count() <= 0)
                                {
                                    if (Sep1 != "00000")
                                    {
                                        string display = "Fax Number Is Duplicate!";
                                        ClientScript.RegisterStartupScript(this.GetType(), "Fax Number Is Duplicate!", "alert('" + display + "');", true);
                                        return;
                                    }
                                }
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
                            int Length = Sep2.Length;
                            if (Length < 7)
                            {
                                PanelError.Visible = true;
                                lblerror.Text = "Please Enter Valid Bus Phone";
                                return;
                            }
                            string str = Sep2.Substring(0, 2);
                            var exist = DB.Tbl_RecordType_Mst.Where(c => c.RecValue == Sep2 && c.CompniyAndContactID == COMPID && c.RecordType == "BusPhone" && c.TenentID == TID && c.Recource == 5005 && c.CompniyAndContactID != COMPID);
                            if (exist.Count() <= 0)
                            {

                                int Countryid = Convert.ToInt32(drpCountry.SelectedValue);
                                if (DB.tblCOUNTRies.Where(p => p.TenentID == TID && p.COUNTRYID == Countryid && p.TelLength != null).Count() > 0)
                                {
                                    int checklen = Convert.ToInt32(DB.tblCOUNTRies.Single(p => p.TenentID == TID && p.COUNTRYID == Countryid && p.TelLength != null).TelLength);
                                    if (str == "18")
                                    {
                                        if (Length < 7)
                                        {
                                            PanelError.Visible = true;
                                            lblerror.Text = "Please Enter minimum 7 Digit of TollFree No In Bus Phone";
                                            return;
                                        }
                                    }
                                }

                                //else
                                //{
                                //    if (Length < 8)
                                //    {
                                //        PanelError.Visible = true;
                                //        lblerror.Text = "Please Enter minimum 8 Digit of Bus Phone";
                                //        return;
                                //    }
                                //}

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

                                String url2 = "insert new record in Tbl_RecordType_Mst with " + "TenentID = " + TID + "COMPID =" + COMPID;
                                String evantname3 = "create";
                                String tablename4 = "Tbl_RecordType_Mst";
                                string loginUserId5 = (((USER_MST)HttpContext.Current.Session["USER"]).USER_ID).ToString();

                                Classes.GlobleClass.EncryptionHelpers.WriteLog(url2, evantname3, tablename4, loginUserId5.ToString(), 0,0);


                            }
                            else
                            {
                                if (exist.Count() <= 0)
                                {
                                    if (Sep2 != "00000")
                                    {
                                        string display = "Bus Number Is Duplicate!";
                                        ClientScript.RegisterStartupScript(this.GetType(), "Bus Number Is Duplicate!", "alert('" + display + "');", true);
                                        return;
                                    }
                                }
                            }
                        }
                    }

                    if (txtMobileNo.Text != "")
                    {
                        //MobileNO
                        List<Tbl_RecordType_Mst> List2;
                        if (COMPID != null && COMPID != 0)
                            List2 = DB.Tbl_RecordType_Mst.Where(p => p.TenentID == TID && p.CompniyAndContactID == COMPID && p.Recource == 5005 && p.RecordType == "MobileNO").ToList();

                        else
                            List2 = DB.Tbl_RecordType_Mst.Where(p => p.TenentID == TID && p.CompniyAndContactID == CID && p.Recource == 5005 && p.RecordType == "MobileNO").ToList();

                        foreach (Database.Tbl_RecordType_Mst item in List2)
                        {
                            // var Deleteobj = DB.Tbl_RecordType_Mst.SingleOrDefault(p => p.CompniyID == COMPID && p.RecTypeID == 5003 && p.RecordType == "BusPhone");
                            DB.Tbl_RecordType_Mst.DeleteObject(item);
                            DB.SaveChanges();
                        }

                        string[] Seperate2 = txtMobileNo.Text.Split(',');
                        int count2 = 0;
                        string Sep2 = "";
                        for (int i = 0; i <= Seperate2.Count() - 1; i++)
                        {
                            Sep2 = Seperate2[i];
                            string str = Sep2.Substring(0, 2);

                            var exist = DB.Tbl_RecordType_Mst.Where(c => c.RecValue == Sep2 && c.CompniyAndContactID == COMPID && c.RecordType == "MobileNO" && c.TenentID == TID && c.Recource == 5005 && c.CompniyAndContactID != COMPID);
                            if (exist.Count() <= 0)
                            {
                                int Length = Sep2.Length;
                                int Countryid = Convert.ToInt32(drpCountry.SelectedValue);
                                if (DB.tblCOUNTRies.Where(p => p.TenentID == TID && p.COUNTRYID == Countryid && p.TelLength != null).Count() > 0)
                                {
                                    int checklen = Convert.ToInt32(DB.tblCOUNTRies.Single(p => p.TenentID == TID && p.COUNTRYID == Countryid && p.TelLength != null).TelLength);
                                    if (str == "18")
                                    {
                                        if (Length < 7)
                                        {
                                            PanelError.Visible = true;
                                            lblerror.Text = "Please Enter minimum 7 Digit of TollFree No In Mobile";
                                            return;
                                        }
                                    }
                                    else if (Length < 8)
                                    {
                                        PanelError.Visible = true;
                                        lblerror.Text = "Please Enter minimum 8 Digit of Mobile NO";
                                        return;
                                    }
                                }
                                else
                                {
                                    if (str == "18")
                                    {
                                        if (Length < 7)
                                        {
                                            PanelError.Visible = true;
                                            lblerror.Text = "Please Enter minimum 7 Digit of TollFree No";
                                            return;
                                        }
                                    }
                                    else if (Length < 8)
                                    {
                                        PanelError.Visible = true;
                                        lblerror.Text = "Please Enter minimum 8 Digit of Mobile NO";
                                        return;
                                    }
                                }

                                count2++;
                                Tbl_RecordType_Mst obj = new Tbl_RecordType_Mst();
                                obj.TenentID = TID;
                                obj.RecordType = "MobileNO";
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

                                String url2 = "insert new record in Tbl_RecordType_Mst with " + "TenentID = " + TID + "COMPID =" + COMPID;
                                String evantname3 = "create";
                                String tablename4 = "Tbl_RecordType_Mst";
                                string loginUserId5 = (((USER_MST)HttpContext.Current.Session["USER"]).USER_ID).ToString();

                                Classes.GlobleClass.EncryptionHelpers.WriteLog(url2, evantname3, tablename4, loginUserId5.ToString(), 0,0);

                            }
                            else
                            {
                                if (exist.Count() <= 0)
                                {
                                    if (Sep2 != "00000")
                                    {
                                        string display = "Mobile NO Is Duplicate!";
                                        ClientScript.RegisterStartupScript(this.GetType(), "Mobile NO Is Duplicate!", "alert('" + display + "');", true);
                                        return;
                                    }
                                }
                            }
                        }
                    }

                    if (ViewState["ListtblCONTACTBus"] != null)
                    {
                        var Listbus = ((List<Database.tblCONTACTBu>)ViewState["ListtblCONTACTBus"]).ToList();
                        for (int i = 0; i < Listbus.Count(); i++)
                        {
                            int Tenent = Convert.ToInt32(Listbus[i].TenentID);
                            int MYID = Convert.ToInt32(Listbus[i].ContactMyID);
                            int Comp = Convert.ToInt32(Listbus[i].CompID);
                            if (DB.tblCONTACTBus.Where(p => p.TenentID == Tenent && p.ContactMyID == MYID && p.CompID == Comp).Count() == 0)
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

                                String url2 = "insert new record in tblCONTACTBu with " + "TenentID = " + TID + "ContactMyID =" + objtbl_CONTACTBus.ContactMyID + "CompID =" + objtbl_CONTACTBus.CompID;
                                String evantname3 = "create";
                                String tablename4 = "tblCONTACTBu";
                                string loginUserId5 = (((USER_MST)HttpContext.Current.Session["USER"]).USER_ID).ToString();

                                Classes.GlobleClass.EncryptionHelpers.WriteLog(url2, evantname3, tablename4, loginUserId5.ToString(), 0,0);
                            }

                        }
                        ViewState["ListtblCONTACTBus"] = null;
                    }
                    //   btnSubmit.Text = "Add";

                    Classes.Toastr.ShowToast(Page, Classes.Toastr.ToastType.Success, "Data Edit Successfully", "Success!", Classes.Toastr.ToastPosition.TopCenter);
                    scope.Complete();
                    //LinkButton13.Visible = false;
                    Response.Redirect("CompanyMaster.aspx?MID=gT9lZ6RqnrhwNtQwQSazyA==");
                }
                else
                {
                    bool EDTFalge = false;
                    int CID1 = Convert.ToInt32(txtCustoID.Text);
                    if (DB.TBLCOMPANYSETUPs.Where(p => p.TenentID == TID && p.COMPID == CID1).Count() > 0)
                    {
                        //PanelError.Visible = true;
                        //lblerror.Text = "Customer ID Already Exist";
                        //return;
                        EDTFalge = true;
                    }
                    int COMPID = Convert.ToInt32(txtCustoID.Text);
                    int SeqCount = DB.TBLCOMPANYSETUPs.Where(p => p.TenentID == TID).Count();
                    Database.TBLCOMPANYSETUP objtbl_COMPANYSETUP = new Database.TBLCOMPANYSETUP();
                    objtbl_COMPANYSETUP.TenentID = TID;
                    objtbl_COMPANYSETUP.COMPID = COMPID;// DB.TBLCOMPANYSETUPs.Where(p => p.TenentID == TID).Count() > 0 ? Convert.ToInt32(DB.TBLCOMPANYSETUPs.Where(p => p.TenentID == TID).Max(p => p.COMPID) + 1) : 1;
                    ViewState["CIDRF"] = Convert.ToInt32(objtbl_COMPANYSETUP.COMPID);
                    objtbl_COMPANYSETUP.PACI = txtPACI.Text;
                    objtbl_COMPANYSETUP.PHYSICALLOCID = drplocation.SelectedValue;
                    objtbl_COMPANYSETUP.COMPNAME1 = txtCustomerName.Text;
                    objtbl_COMPANYSETUP.COMPNAME2 = txtCustomer.Text;
                    objtbl_COMPANYSETUP.COMPNAME3 = txtCustomer2.Text;
                    objtbl_COMPANYSETUP.EMAIL1 = getFirstCommaseprate(tags_2.Text);
                    // objtbl_COMPANYSETUP.EMAIL2 = tags_2.Text;
                    objtbl_COMPANYSETUP.ITMANAGER = drpItManager.SelectedValue;
                    objtbl_COMPANYSETUP.ADDR1 = txtAddress.Text;
                    objtbl_COMPANYSETUP.ADDR2 = txtAddress2.Text;
                    if (txtBirthdate.Text != "")
                    {
                        objtbl_COMPANYSETUP.BirthDate = Convert.ToDateTime(txtBirthdate.Text);
                    }

                    objtbl_COMPANYSETUP.CivilID = txtCivilID.Text;
                    //objtbl_COMPANYSETUP.CITY = txtCity.Text;
                    objtbl_COMPANYSETUP.CITY = drpcity.SelectedValue;
                    objtbl_COMPANYSETUP.STATE = drpSates.SelectedValue;
                    objtbl_COMPANYSETUP.POSTALCODE = txtPostalCode.Text;
                    objtbl_COMPANYSETUP.ZIPCODE = txtZipCode.Text;
                    //objtbl_COMPANYSETUP.MYCONLOCID = Convert.ToInt32(drpMyCounLocID.SelectedValue);
                    objtbl_COMPANYSETUP.MYPRODID = Convert.ToInt32(drpMyProductId.SelectedValue);
                    //  string CNAME = drpCountry.SelectedItem.ToString();
                    objtbl_COMPANYSETUP.COUNTRYID = Convert.ToInt32(drpCountry.SelectedValue);
                    objtbl_COMPANYSETUP.BUSPHONE1 = getFirstCommaseprate(tags_4.Text);
                    //objtbl_COMPANYSETUP.BUSPHONE2 = tags_4.Text;
                    //objtbl_COMPANYSETUP.BUSPHONE3 = tags_4.Text;
                    //objtbl_COMPANYSETUP.BUSPHONE4 = tags_4.Text;
                    objtbl_COMPANYSETUP.MOBPHONE = getFirstCommaseprate(txtMobileNo.Text);

                    objtbl_COMPANYSETUP.FAX = getFirstCommaseprate(tags_3.Text);
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
                    objtbl_COMPANYSETUP.Marketting = txtrefreshno.Text;
                    objtbl_COMPANYSETUP.CUSERID = txtcUserName.Text;
                    objtbl_COMPANYSETUP.CPASSWRD = txtcPassword.Text;
                    objtbl_COMPANYSETUP.USERID = UID;
                    objtbl_COMPANYSETUP.ENTRYDATE = DateTime.Now;
                    objtbl_COMPANYSETUP.ENTRYTIME = DateTime.Now;
                    objtbl_COMPANYSETUP.UPDTTIME = DateTime.Now;

                    if (drpdatasource.SelectedValue != "0")
                        objtbl_COMPANYSETUP.datasource = Convert.ToInt32(drpdatasource.SelectedValue);

                    objtbl_COMPANYSETUP.Approved = 0;
                    objtbl_COMPANYSETUP.Active = "Y";

                    if (avatarUploadd.HasFile)
                    {
                        string path = COMPID + "_" + TID + "_" + txtCustomerName.Text + "_TBLCOMPANYSETUP_" + avatarUploadd.FileName;
                        avatarUploadd.SaveAs(Server.MapPath("/CRM/Upload/" + path));
                        objtbl_COMPANYSETUP.Avtar = path;
                    }
                    if (txtBarCode.Text != "")
                    {
                        //string str = (Server.MapPath("/CRM/images/" + txtBarCode.Text + ".png"));                       
                        objtbl_COMPANYSETUP.BARCODE = txtBarCode.Text;

                    }
                    if (EDTFalge == false)
                    {
                        DB.TBLCOMPANYSETUPs.AddObject(objtbl_COMPANYSETUP);
                    }
                    int crup = GlobleClass.EncryptionHelpers.WriteLog("CRM_Company_Add New Company(INSERT):", "Login", "TBLCOMPANYSETUP", UID.ToString(), 0,0);
                    objtbl_COMPANYSETUP.CRUP_ID = Convert.ToInt64(crup);
                    DB.SaveChanges();

                    String url2 = "insert new record in TBLCOMPANYSETUP with " + "TenentID = " + TID + "COMPID =" + COMPID ;
                    String evantname3 = "create";
                    String tablename4 = "TBLCOMPANYSETUP";
                    string loginUserId5 = (((USER_MST)HttpContext.Current.Session["USER"]).USER_ID).ToString();

                    Classes.GlobleClass.EncryptionHelpers.WriteLog(url2, evantname3, tablename4, loginUserId5.ToString(), 0,0);
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

                            String url = "insert new record in Tbl_RecordType_Mst with " + "TenentID = " + TID + "RecordType = Classification" + "RecTypeID =" + obj.RecTypeID;
                            String evantname = "create";
                            String tablename = "Tbl_RecordType_Mst";
                            string loginUserId = (((USER_MST)HttpContext.Current.Session["USER"]).USER_ID).ToString();

                            Classes.GlobleClass.EncryptionHelpers.WriteLog(url, evantname, tablename, loginUserId.ToString(), 0,0);
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

                            String url = "insert new record in Tbl_RecordType_Mst with " + "TenentID = " + TID + "RecordType = BRAND" + "RecTypeID =" + obj.RecTypeID;
                            String evantname = "create";
                            String tablename = "Tbl_RecordType_Mst";
                            string loginUserId = (((USER_MST)HttpContext.Current.Session["USER"]).USER_ID).ToString();

                            Classes.GlobleClass.EncryptionHelpers.WriteLog(url, evantname, tablename, loginUserId.ToString(), 0,0);
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
                                int Length = Sep1.Length;
                                int Countryid = Convert.ToInt32(drpCountry.SelectedValue);
                                if (DB.tblCOUNTRies.Where(p => p.TenentID == TID && p.COUNTRYID == Countryid && p.FaxLength != null).Count() > 0)
                                {
                                    int checklen = Convert.ToInt32(DB.tblCOUNTRies.Single(p => p.TenentID == TID && p.COUNTRYID == Countryid && p.FaxLength != null).FaxLength);
                                    if (Length < 8)
                                    {
                                        PanelError.Visible = true;
                                        lblerror.Text = "Please Enter minimum 8 Digit of Fax";
                                        return;
                                    }
                                }
                                else
                                {
                                    if (Length < 8)
                                    {
                                        PanelError.Visible = true;
                                        lblerror.Text = "Please Enter minimum 8 Digit of Fax";
                                        return;
                                    }
                                }
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
                            int Length = Sep2.Length;
                            if (Length < 7)
                            {
                                PanelError.Visible = true;
                                lblerror.Text = "Please Enter Valid Bus Phone";
                                return;
                            }
                            string str = Sep2.Substring(0, 2);

                            var exist = DB.Tbl_RecordType_Mst.Where(c => c.RecValue == Sep2 && c.RecourceName == "Company" && c.RecordType == "BusPhone" && c.TenentID == TID);
                            if (exist.Count() <= 0)
                            {

                                int Countryid = Convert.ToInt32(drpCountry.SelectedValue);
                                if (DB.tblCOUNTRies.Where(p => p.TenentID == TID && p.COUNTRYID == Countryid && p.TelLength != null).Count() > 0)
                                {
                                    int checklen = Convert.ToInt32(DB.tblCOUNTRies.Single(p => p.TenentID == TID && p.COUNTRYID == Countryid && p.TelLength != null).TelLength);
                                    if (str == "18")
                                    {
                                        if (Length < 7)
                                        {
                                            PanelError.Visible = true;
                                            lblerror.Text = "Please Enter minimum 7 Digit of TollFree No In Bus Phone";
                                            return;
                                        }
                                    }
                                }
                                //else
                                //{
                                //    if (Length < 8)
                                //    {
                                //        PanelError.Visible = true;
                                //        lblerror.Text = "Please Enter minimum 8 Digit of Bus Phone";
                                //        return;
                                //    }
                                //}
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

                    if (txtMobileNo.Text != "")
                    {
                        //MobileNO
                        List<Tbl_RecordType_Mst> List2;
                        if (COMPID != null && COMPID != 0)
                            List2 = DB.Tbl_RecordType_Mst.Where(p => p.TenentID == TID && p.CompniyAndContactID == COMPID && p.Recource == 5005 && p.RecordType == "MobileNO").ToList();

                        else
                            List2 = DB.Tbl_RecordType_Mst.Where(p => p.TenentID == TID && p.CompniyAndContactID == CID && p.Recource == 5005 && p.RecordType == "MobileNO").ToList();

                        foreach (Database.Tbl_RecordType_Mst item in List2)
                        {
                            // var Deleteobj = DB.Tbl_RecordType_Mst.SingleOrDefault(p => p.CompniyID == COMPID && p.RecTypeID == 5003 && p.RecordType == "BusPhone");
                            DB.Tbl_RecordType_Mst.DeleteObject(item);
                            DB.SaveChanges();
                        }

                        string[] Seperate2 = txtMobileNo.Text.Split(',');
                        int count2 = 0;
                        string Sep2 = "";
                        for (int i = 0; i <= Seperate2.Count() - 1; i++)
                        {
                            Sep2 = Seperate2[i];
                            int Length = Sep2.Length;
                            if (Length < 7)
                            {
                                PanelError.Visible = true;
                                lblerror.Text = "Please Enter Valid Mobile No.";
                                return;
                            }
                            string str = Sep2.Substring(0, 2);
                            var exist = DB.Tbl_RecordType_Mst.Where(c => c.RecValue == Sep2 && c.CompniyAndContactID == COMPID && c.RecordType == "MobileNO" && c.TenentID == TID && c.Recource == 5005 && c.CompniyAndContactID != COMPID);
                            if (exist.Count() <= 0)
                            {

                                int Countryid = Convert.ToInt32(drpCountry.SelectedValue);
                                if (DB.tblCOUNTRies.Where(p => p.TenentID == TID && p.COUNTRYID == Countryid && p.TelLength != null).Count() > 0)
                                {
                                    int checklen = Convert.ToInt32(DB.tblCOUNTRies.Single(p => p.TenentID == TID && p.COUNTRYID == Countryid && p.TelLength != null).TelLength);
                                    if (str == "18")
                                    {
                                        if (Length < 7)
                                        {
                                            PanelError.Visible = true;
                                            lblerror.Text = "Please Enter minimum 7 Digit of TollFree No In Mobile";
                                            return;
                                        }
                                    }
                                    else if (Length < 8)
                                    {
                                        PanelError.Visible = true;
                                        lblerror.Text = "Please Enter minimum 8 Digit of Mobile NO";
                                        return;
                                    }
                                }
                                else
                                {
                                    if (str == "18")
                                    {
                                        if (Length < 7)
                                        {
                                            PanelError.Visible = true;
                                            lblerror.Text = "Please Enter minimum 7 Digit of TollFree No";
                                            return;
                                        }
                                    }
                                    else if (Length < 8)
                                    {
                                        PanelError.Visible = true;
                                        lblerror.Text = "Please Enter minimum 8 Digit of Mobile NO";
                                        return;
                                    }
                                }

                                count2++;
                                Tbl_RecordType_Mst obj = new Tbl_RecordType_Mst();
                                obj.TenentID = TID;
                                obj.RecordType = "MobileNO";
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
                                if (exist.Count() <= 0)
                                {
                                    if (Sep2 != "00000")
                                    {
                                        string display = "Mobile NO Is Duplicate!";
                                        ClientScript.RegisterStartupScript(this.GetType(), "Mobile NO Is Duplicate!", "alert('" + display + "');", true);
                                        return;
                                    }
                                }
                            }
                        }
                    }

                    if (ViewState["ListtblCONTACTBus"] != null)
                    {
                        var Listbus = ((List<Database.tblCONTACTBu>)ViewState["ListtblCONTACTBus"]).ToList();
                        for (int i = 0; i < Listbus.Count(); i++)
                        {
                            int Tenent = Convert.ToInt32(Listbus[i].TenentID);
                            int MYID = Convert.ToInt32(Listbus[i].ContactMyID);
                            int Comp = Convert.ToInt32(Listbus[i].CompID);
                            if (DB.tblCONTACTBus.Where(p => p.TenentID == Tenent && p.ContactMyID == MYID && p.CompID == Comp).Count() == 0)
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

                        }
                        ViewState["ListtblCONTACTBus"] = null;
                    }

                    Classes.Toastr.ShowToast(Page, Classes.Toastr.ToastType.Success, "Data Save Successfully", "Success!", Classes.Toastr.ToastPosition.TopCenter);

                }
                btnSaveandConti.Visible = false;
                NullBullContact();
                BindData();
                // clen();
                redonlyfalse();
                btnSubmit.Visible = false;
                Button1.Visible = true;
                txtCustoID.Enabled = false;
                btnattmentmst.Visible = false;
                PanalFBImage.Visible = false;
                scope.Complete(); //  To commit.

            }


        }

        private System.Drawing.Image MakeBarcodeImage(string p1, int p2, bool p3)
        {
            throw new NotImplementedException();
        }
        public void BindDataClassfic()
        {
            Classes.CRMClass.getcrmdropdown(drpCategory, TID, "7861", "1", "", "CAT_MST");
            Classes.CRMClass.getcrmdropdown(drpmarketing, TID, "7861", "1", "", "CAT_MST");
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

            //Classes.EcommAdminClass.getdropdown(drpMyCounLocID, TID, "", "", "", "TBLCOMPANY_LOCATION");
            Classes.EcommAdminClass.getdropdown(drplocation, TID, "1", "", "", "TBLCOMPANY_LOCATION");
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

            drpSort.Items.Clear();
            drpSort.Items.Insert(0, new ListItem("---Sorting By---", "0"));
            drpSort.Items.Insert(1, new ListItem(lblcustemername.Text, "1"));
            drpSort.Items.Insert(2, new ListItem(lblClass.Text, "2"));
            drpSort.Items.Insert(3, new ListItem(lblmobiellis.Text, "3"));
            drpSort.Items.Insert(4, new ListItem(lblstatelis.Text, "4"));
            drpSort.Items.Insert(5, new ListItem(lblcitylit.Text, "5"));
            //drpSort.Items.Insert(2, new ListItem(lbladdresh.Text, "2"));
            //drpSort.Items.Insert(2, new ListItem(lblemaillist.Text, "3"));
            //drpSort.Items.Insert(5, new ListItem(lblzipcodelit.Text, "6"));

            drpcity.DataSource = DB.tblCityStatesCounties.OrderBy(p => p.CityEnglish);
            drpcity.DataTextField = "CityEnglish";
            drpcity.DataValueField = "CityID";
            drpcity.DataBind();
            drpcity.Items.Insert(0, new ListItem("-- Select --", "0"));

            drpdatasource.DataSource = DB.REFTABLEs.Where(p => p.TenentID == TID && p.REFTYPE == "CRM" && p.REFSUBTYPE == "CONTACTSOURCE").OrderBy(p=>p.REFNAME1); //DB.TBLCOMPANYSETUPs.Where(p => p.TenentID == TID).OrderBy(p => p.COMPNAME1);
            drpdatasource.DataTextField = "REFNAME1";
            drpdatasource.DataValueField = "REFID";
            drpdatasource.DataBind();
            drpdatasource.Items.Insert(0, new ListItem("-- Select --", "0"));

            Classes.EcommAdminClass.getdropdown(drptemplete, TID, "", "", "", "Mail_Templet");
            Classes.EcommAdminClass.getdropdown(DrpPopPosition, TID, "", "", "", "Tbl_Position_Mst");

            drpAPOStatus.Items[1].Attributes.CssStyle.Add("background-color", "red");
            drpAPOStatus.Items[2].Attributes.CssStyle.Add("background-color", "green");
            drpAPOStatus.Items[3].Attributes.CssStyle.Add("background-color", "blue");
            drpAPOStatus.Items[4].Attributes.CssStyle.Add("background-color", "yellow");
            drpAPOStatus.Items[5].Attributes.CssStyle.Add("background-color", "purple");
            drpAPOStatus.Items[6].Attributes.CssStyle.Add("background-color", "gray");
            drpAPOStatus.Items[7].Attributes.CssStyle.Add("background-color", "indigo");
            drpAPOStatus.Items[8].Attributes.CssStyle.Add("background-color", "aqua");
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            redonlyfalse();
            btnSubmit.Visible = false;
            Button1.Visible = true;
            txtCustoID.Enabled = false;
            // btnSubmit.Text = "New";           

            //Lastdataredmode();
            btnattmentmst.Visible = false;
            PanalFBImage.Visible = false;
            int Countt = DB.TBLCOMPANYSETUPs.Where(p => p.TenentID == TID).Count();
            if (Countt != 0)
            {
                if (DB.CRMUserSetups.Where(p => p.TenentID == TID && p.userid == UID).Count() > 0)
                {

                    CRMUserSetup obj = DB.CRMUserSetups.Single(p => p.TenentID == TID && p.userid == UID);
                    chDefaultSet.Checked = obj.Active == true ? true : false;
                    if (obj.Active == true)
                    {
                        DrpTitle.SelectedValue = obj.startupCompRefID.ToString();
                        SetDefaultGrid();
                    }

                }
            }
            LinkAddEmployee.Attributes["Style"] = "display:none;";
            NullBullContact();
            btnSaveandConti.Visible = false;
        }
        public void NullBullContact()
        {
            List<Database.tblCONTACTBu> BusList = new List<Database.tblCONTACTBu>();
            lstCompniy.DataSource = BusList;
            lstCompniy.DataBind();
        }
        public string getStste(int SID, int CID)
        {
            return DB.tblStates.Where(p => p.StateID == SID && p.COUNTRYID == CID).Select(p => p.MYNAME1).FirstOrDefault();
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
            if (btnSubmit.Text == "Save" && btnSubmit.Visible == true)
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
            {
                //return DB.TBLCOMPANYSETUPs.SingleOrDefault(p => p.COMPID == GCID && p.TenentID == TID).CITY;
                Database.TBLCOMPANYSETUP COBJ = DB.TBLCOMPANYSETUPs.SingleOrDefault(p => p.COMPID == GCID && p.TenentID == TID);
                int Cityidd = Convert.ToInt32(COBJ.CITY);
                int Statett = Convert.ToInt32(COBJ.STATE);
                int countrry = Convert.ToInt32(COBJ.COUNTRYID);
                List<Database.tblCityStatesCounty> CityList = DB.tblCityStatesCounties.Where(p => p.CityID == Cityidd && p.StateID == Statett && p.COUNTRYID == countrry).ToList();
                if (CityList.Count() == 1)
                {
                    return CityList[0].CityEnglish;
                }
                else
                {
                    return drpcity.SelectedItem.ToString();
                }
            }
            else
            {
                return drpcity.SelectedItem.ToString();// txtCity.Text;
            }

        }

        public string getCityName(int City, int StateID, int cid)
        {
            if (DB.tblCityStatesCounties.Where(p => p.StateID == StateID && p.CityID == City && p.COUNTRYID == cid).Count() > 0)
            {
                return DB.tblCityStatesCounties.Single(p => p.StateID == StateID && p.CityID == City && p.COUNTRYID == cid).CityEnglish;
            }
            else
            {
                return "Not Found";
            }
        }
        public string getstate(int GCID)
        {
            if (DB.TBLCOMPANYSETUPs.Where(p => p.COMPID == GCID && p.TenentID == TID).Count() > 0)
            {
                int Stateidd = Convert.ToInt32(DB.TBLCOMPANYSETUPs.SingleOrDefault(p => p.COMPID == GCID && p.TenentID == TID).STATE);
                List<Database.tblState> stateList = DB.tblStates.Where(p => p.StateID == Stateidd).ToList();
                if (stateList.Count() == 1)
                {
                    return stateList[0].MYNAME1;
                }
                else
                {
                    return drpSates.SelectedItem.ToString();
                }
            }
            else
            {
                return drpSates.SelectedItem.ToString();
            }

        }
        protected void txtCustomerName_TextChanged(object sender, EventArgs e)
        {
            if (btnSubmit.Text == "Save" && btnSubmit.Visible == true)
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
        }

        protected void btnYes_Click(object sender, EventArgs e)
        {
            int CID = Convert.ToInt32(ViewState["compId"]);

            txtCustoID.Text = CID.ToString();//
            txtCustoID.Enabled = false;//
            string Cname = GetCustomerName(CID);
            BindEditcompniy(CID);
            updCompny.Update();
            redonlyture();
            lblBusContactDe.Text = "Business Company Details -Edit Mode - " + Cname;
            lblBCDeta.Text = "Business Company Details -Edit Mode - " + Cname;
            lblwExistance.Text = "Web Existance -Edit Mode - " + Cname;
            lblWEmp.Text = "Working Employees -Edit Mode - " + Cname;

            Session["ADMInPrevious"] = Session["ADMInCurrent"];//
            Session["ADMInCurrent"] = Request.RawUrl;//                     
            btnSubmit.Visible = true;//
            Button1.Visible = false;//
            btnSubmit.Text = "Update";

            btnattmentmst.Visible = true;//
            PanalFBImage.Visible = true;//
            lblAttecmentcount.Text = Countatt(CID);//
            LinkAddEmployee.Attributes["Style"] = "display:block;";//
            btnSaveandConti.Text = "Update & continues";//
            btnSaveandConti.Visible = true;//
        }

        protected void btnNo_Click(object sender, EventArgs e)
        {

        }

        protected void txtCustomer_TextChanged(object sender, EventArgs e)
        {
            if (btnSubmit.Text == "Save" && btnSubmit.Visible == true)
            {
                lblCustomerL1.Text = "";
                if (!string.IsNullOrEmpty(txtCustomer.Text))
                {
                    var exist = DB.TBLCOMPANYSETUPs.Where(c => c.COMPNAME2 == txtCustomer.Text && c.TenentID == TID);
                    var UserList = DB.TBLCOMPANYSETUPs.FirstOrDefault(p => p.COMPNAME2 == txtCustomer.Text && p.TenentID == TID);

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
        }

        protected void txtCustomer2_TextChanged(object sender, EventArgs e)
        {
            if (btnSubmit.Text == "Save" && btnSubmit.Visible == true)
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
        }

        protected void txtMobileNo_TextChanged(object sender, EventArgs e)
        {
            if (btnSubmit.Text == "Save" && btnSubmit.Visible == true)
            {
                lblMobileNo.Text = "";
                if (!string.IsNullOrEmpty(txtMobileNo.Text))
                {
                    var exist = DB.TBLCOMPANYSETUPs.Where(c => c.MOBPHONE == txtMobileNo.Text && c.TenentID == TID);
                    var UserList = DB.TBLCOMPANYSETUPs.SingleOrDefault(p => p.MOBPHONE == txtMobileNo.Text && p.TenentID == TID);

                    if (exist.Count() <= 0)
                    {
                        lblMobileNo.Text = "Mobile Number is Available";
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
                    lblMobileNo.Text = "Insert The Mobile Number !";
                }
            }
        }

        protected void txtcUserName_TextChanged(object sender, EventArgs e)
        {
            if (btnSubmit.Text == "Save" && btnSubmit.Visible == true)
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
            //txtCity.Text = (Listview1.SelectedDataKey[7]).ToString();
            drpcity.SelectedValue = (Listview1.SelectedDataKey[7]).ToString();
            drpSates.SelectedValue = (Listview1.SelectedDataKey[8]).ToString();
            txtPostalCode.Text = (Listview1.SelectedDataKey[9]).ToString();
            txtZipCode.Text = (Listview1.SelectedDataKey[10]).ToString();
            //drpMyCounLocID.SelectedValue = (Listview1.SelectedDataKey[11]).ToString();
            drpMyProductId.SelectedValue = (Listview1.SelectedDataKey[12]).ToString();
            drpCountry.SelectedValue = (Listview1.SelectedDataKey[13]).ToString();
            tags_4.Text = (Listview1.SelectedDataKey[14]).ToString();
            txtMobileNo.Text = (Listview1.SelectedDataKey[15]).ToString();
            tags_3.Text = (Listview1.SelectedDataKey[16]).ToString();
            // int PLID = Convert.ToInt32(objtbl_COMPANYSETUP.PRIMLANGUGE);
            drpPrimaryLang.SelectedValue = (Listview1.SelectedDataKey[17]).ToString();
            txtWebsite.Text = (Listview1.SelectedDataKey[18]).ToString();
            if (Listview1.SelectedDataKey[19] != null)
            {
                int Refid = Convert.ToInt32(Listview1.SelectedDataKey[19]);
                List<Database.REFTABLE> CType = DB.REFTABLEs.Where(p => p.TenentID == TID && p.REFID == Refid && p.REFTYPE == "COMP" && p.REFSUBTYPE == "COMPTYPE" && p.ACTIVE == "Y").ToList();
                if (CType.Count() > 0)
                    drpType.SelectedValue = (Listview1.SelectedDataKey[19]).ToString();
            }


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
            txtBirthdate.Text = (Listview1.SelectedDataKey[32]).ToString();
            txtCivilID.Text = (Listview1.SelectedDataKey[33]).ToString();

            // int COID = objtbl_COMPANYSETUP.COUNTRYID;




        }

        protected void btnBared_Click(object sender, EventArgs e)
        {
            string drpval = drpbrand.SelectedItem.ToString();
            string tagdt = "";

            if (tags_5.Text != "")
            {
                tagdt = tags_5.Text;
                tagdt += "," + drpval;
                tags_5.Text = tagdt;

            }
            else
            {
                tagdt = drpval;
                tags_5.Text = tagdt;

            }

            ViewState["SaveList2"] = tags_5.Text;
        }

        protected void drpCompnay_SelectedIndexChanged(object sender, EventArgs e)
        {
            int CID = Convert.ToInt32(drpCompnay.SelectedValue);
            int CCIDD = Convert.ToInt32(txtCustoID.Text);
            if (ViewState["ListtblCONTACTBus"] != null)
            {
                ListtblCONTACTBus = (List<Database.tblCONTACTBu>)ViewState["ListtblCONTACTBus"];
            }
            if (ListtblCONTACTBus.Where(p => p.TenentID == TID && p.ContactMyID == CID && p.CompID == CCIDD).Count() > 0)
            {
                string msg = "This Employee Is Allready Exist...";
                string Function = "openModal('" + msg + "');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", Function, true);
            }

            //BindConmpnicontect(CID);
        }
        public void BindConmpnicontect(int CID)
        {
            lstCompniy.DataSource = DB.tblCONTACTBus.Where(p => p.TenentID == TID && p.Active == "Y" && p.ContactMyID == CID);
            lstCompniy.DataBind();


        }
        public void BindCompanywisecontact(int CID)
        {
            List<Database.tblCONTACTBu> CList = DB.tblCONTACTBus.Where(p => p.TenentID == TID && p.Active == "Y" && p.CompID == CID).ToList();
            lstCompniy.DataSource = CList;
            lstCompniy.DataBind();
            ViewState["ListtblCONTACTBus"] = CList;
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
                        List<Database.REFTABLE> List_Ref = DB.REFTABLEs.Where(p => p.REFTYPE == RefType && p.REFSUBTYPE == RefSubType && p.TenentID == TID).ToList();
                        if (List_Ref.Where(p => p.REFNAME1 == txtTitle.Text && p.TenentID == TID).Count() < 1)
                        {
                            int RID = DB.REFTABLEs.Where(p => p.TenentID == TID).Count() > 0 ? Convert.ToInt32(DB.REFTABLEs.Where(p => p.TenentID == TID).Max(p => p.REFID) + 1) : 1;

                            bool flag = false;
                            if (ViewState["SearchView"] != null)
                            {
                                var Listserch = ((List<Database.View_CompanyMaster>)ViewState["SearchView"]).ToList();
                                for (int i = 0; i < Listserch.Count; i++)
                                {
                                    int HID = Convert.ToInt32(Listserch[i].COMPID);

                                    Database.ISSearchDetail obj_Detail = new Database.ISSearchDetail();
                                    obj_Detail.ID = DB.ISSearchDetails.Where(p => p.TenentID == TID).Count() > 0 ? Convert.ToInt32(DB.ISSearchDetails.Where(p => p.TenentID == TID).Max(p => p.ID) + 1) : 1;
                                    obj_Detail.TenentID = TID;
                                    obj_Detail.LocationID = LID;
                                    obj_Detail.REFID = RID;
                                    obj_Detail.CompanyID = HID;
                                    obj_Detail.CreatedBy = UID;
                                    obj_Detail.Active = true;
                                    obj_Detail.Deleted = true;
                                    if (DB.ISSearchDetails.Where(p => p.CompanyID == HID && p.CreatedBy == UID && p.REFID == RID && p.TenentID == TID).Count() < 1)
                                    {
                                        DB.ISSearchDetails.AddObject(obj_Detail);
                                        DB.SaveChanges();
                                        flag = true;
                                    }
                                }
                            }
                            else if (Listview1.Items.Count() > 0)
                            {
                                for (int j = 0; j < Listview1.Items.Count; j++)
                                {
                                    Label Label91 = (Label)Listview1.Items[j].FindControl("Label91");
                                    int HID = Convert.ToInt32(Label91.Text);
                                    Database.ISSearchDetail obj_Detail = new Database.ISSearchDetail();
                                    obj_Detail.ID = DB.ISSearchDetails.Where(p => p.TenentID == TID).Count() > 0 ? Convert.ToInt32(DB.ISSearchDetails.Where(p => p.TenentID == TID).Max(p => p.ID) + 1) : 1;
                                    obj_Detail.TenentID = TID;
                                    obj_Detail.LocationID = LID;
                                    obj_Detail.REFID = RID;
                                    obj_Detail.CompanyID = HID;
                                    obj_Detail.CreatedBy = UID;
                                    obj_Detail.Active = true;
                                    obj_Detail.Deleted = true;
                                    if (DB.ISSearchDetails.Where(p => p.CompanyID == HID && p.CreatedBy == UID && p.REFID == RID && p.TenentID == TID).Count() < 1)
                                    {
                                        DB.ISSearchDetails.AddObject(obj_Detail);
                                        DB.SaveChanges();
                                        flag = true;
                                    }
                                }
                            }
                            //foreach (ListViewItem item in Listview1.Items)
                            //{
                            //    HiddenField HID = (HiddenField)item.FindControl("hidecontactid");
                            //    Con = Convert.ToInt32(HID.Value);
                            //    Database.ISSearchDetail obj_Detail = new ISSearchDetail();
                            //    obj_Detail.ID = DB.ISSearchDetail.Count() > 0 ? Convert.ToInt32(DB.ISSearchDetail.Max(p => p.ID) + 1) : 1;
                            //    obj_Detail.TenentID = TID;
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
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('unselect Saved Search ');", true);
            }
            BindTitleData();
            txtTitle.Text = "";
        }
        public void BindTitleData()
        {
            string REFtype = Convert.ToString(SearchManagement.SearchType.Search);
            string REFSubtype = Convert.ToString(SearchManagement.SearchType.Company);
            DrpTitle.DataSource = DB.REFTABLEs.Where(P => P.REFTYPE == REFtype && P.REFSUBTYPE == REFSubtype && P.TenentID == TID && P.ACTIVE == "Y").OrderBy(a => a.REFNAME1);
            DrpTitle.DataTextField = "REFNAME1";
            DrpTitle.DataValueField = "REFID";
            DrpTitle.DataBind();
            DrpTitle.Items.Insert(0, new ListItem("-- Select --", "0"));
            DrpTitle.Items.Insert(1, new ListItem("Show All", "999999"));
            DrpTitle.SelectedValue = "999999";
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

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            SetDefaultGrid();
            int ID = Convert.ToInt32(DrpTitle.SelectedValue);
            Database.CRMUserSetup obj = null;
            if (chDefaultSet.Checked)
            {
                if (DB.CRMUserSetups.Where(p => p.TenentID == TID && p.userid == UID && p.startupCompRefID != 0).Count() > 0)
                {
                    obj = DB.CRMUserSetups.Single(p => p.TenentID == TID && p.userid == UID && p.startupCompRefID != 0);
                    obj.startupCompRefID = ID;
                    obj.Active = true;

                }
                else
                {
                    obj = new CRMUserSetup();
                    obj.TenentID = TID;
                    obj.userid = UID;
                    obj.startupCompRefID = ID;
                    obj.StartupContactRefID = 0;
                    obj.DateTime = DateTime.Now;
                    obj.Active = true;
                    DB.CRMUserSetups.AddObject(obj);

                }
                DB.SaveChanges();
            }
            else
            {
                if (DB.CRMUserSetups.Where(p => p.TenentID == TID && p.startupCompRefID != 0 && p.userid == UID).Count() > 0)
                {
                    obj = DB.CRMUserSetups.Single(p => p.TenentID == TID && p.startupCompRefID != 0 && p.userid == UID);
                    obj.Active = false;
                    DB.SaveChanges();
                }
            }
        }
        public void SetDefaultGrid()
        {
            if (Convert.ToInt32(DrpTitle.SelectedValue) == 999999)
            {
                BindData();
            }
            else if (Convert.ToInt32(DrpTitle.SelectedValue) != 0)
            {
                int TitleID = Convert.ToInt32(DrpTitle.SelectedValue);

                //var Search_List = DB.ISSearchDetails.Where(p => p.REFID == TitleID && p.CreatedBy == UID && p.TenentID == TID).ToList();

                //List<ISSearchDetail> Search_List = DB.ISSearchDetails.Where(p => p.REFID == TitleID && p.CreatedBy == UID && p.TenentID == TID).ToList();
                List<Database.TBLCOMPANYSETUP> Con_List = DB.TBLCOMPANYSETUPs.Where(p => p.ISSearchDetails.Any(a => a.CompanyID == p.COMPID && a.REFID == TitleID && a.TenentID == TID) && p.TenentID == TID && p.Active == "Y").ToList();



                //List<Database.TBLCOMPANYSETUP> Con_List = new List<TBLCOMPANYSETUP>();

                //List<Database.TBLCOMPANYSETUP> listcompany = DB.TBLCOMPANYSETUPs.Where(p => p.TenentID == TID).ToList();

                //foreach (Database.TBLCOMPANYSETUP item in listcompany)
                //{
                //    List<Database.ISSearchDetail> listissearch = DB.ISSearchDetails.Where(p => p.CompanyID == item.COMPID && p.REFID == TitleID && p.TenentID == TID).ToList();
                //    foreach (Database.ISSearchDetail itemsearch in listissearch)
                //    {
                //        if (itemsearch.CompanyID == item.COMPID)
                //        {
                //            Database.TBLCOMPANYSETUP objcomp = item;
                //            Con_List.Add(objcomp);
                //        }
                //    }
                //}

                //foreach (ISSearchDetail item in Search_List)
                //{

                //    Database.TBLCOMPANYSETUP cust = DB.TBLCOMPANYSETUPs.Where(p => p.COMPID == item.CompanyID && p.TenentID == TID).FirstOrDefault();
                //    if (cust != null)
                //    {
                //        Database.TBLCOMPANYSETUP obj_Contact = cust;
                //        Con_List.Add(obj_Contact);
                //    }

                //}
                ViewState["SearchView"] = null;
                ViewState["Title"] = Con_List;
                //  ViewState["SerchListofCompny"] = List;
                int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
                int Totalrec = Con_List.Count();

                if (Totalrec > 0)
                {
                    BindEditcompniy(Con_List[0].COMPID);
                    //Loadlist(Showdata, take, Skip, ChoiceID, lblShowinfEntry, btnPrevious1, btnNext1, Listview1, ListView2, Totalrec, Con_List);
                    BindData();
                }
            }

        }

        protected void btnlistreload_Click(object sender, EventArgs e)
        {
            BindData();
        }
        protected void drpShowGrid_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (chkactive.Checked == true)
            {
                BindDataActive();
            }
            else
            {
                BindData();
            }
        }

        protected void drpSort_SelectedIndexChanged(object sender, EventArgs e)
        {
            int Countt = 0;
            if (chkactive.Checked == true)
            {
                if (ViewState["SearchView"] != null)
                {
                    List<Database.View_CompanyMaster> List = ((List<Database.View_CompanyMaster>)ViewState["SearchView"]).ToList();
                    Countt = List.Count();

                    if (drpSort.SelectedValue == "1")
                        List = List.OrderBy(m => m.COMPNAME1).Where(p => p.Active == "Y" && p.TenentID == TID).ToList();
                    if (drpSort.SelectedValue == "2")
                        List = List.OrderBy(m => m.Keyword).Where(p => p.Active == "Y" && p.TenentID == TID).ToList();//p.Active == "Y" &&
                    if (drpSort.SelectedValue == "3")
                        List = List.OrderBy(m => m.MOBPHONE).Where(p => p.Active == "Y" && p.TenentID == TID).ToList();//p.Active == "Y" &&
                    if (drpSort.SelectedValue == "4")
                        List = List.OrderBy(m => m.MYNAME1).Where(p => p.Active == "Y" && p.TenentID == TID).ToList();//p.Active == "Y" &&
                    if (drpSort.SelectedValue == "5")
                        List = List.OrderBy(m => m.CityEnglish).Where(p => p.Active == "Y" && p.TenentID == TID).ToList();//p.Active == "Y" &&
                    //if (drpSort.SelectedValue == "4")
                    //    List = List.OrderBy(m => m.STATE).Where(p => p.Active == "Y" && p.TenentID == TID).ToList();//p.Active == "Y" &&
                    //if (drpSort.SelectedValue == "3")
                    //    List = List.OrderBy(m => m.EMAIL1).Where(p => p.Active == "Y" && p.TenentID == TID).ToList();//p.Active == "Y" &&
                    //if (drpSort.SelectedValue == "6")
                    //    List = List.OrderBy(m => m.ZIPCODE).Where(p => p.Active == "Y" && p.TenentID == TID).ToList();//p.Active == "Y" &&


                    int COMPID = List[0].COMPID;
                    int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
                    int Totalrec = Countt;

                    ViewState["SearchView"] = List;
                    BindEditcompniy(COMPID);
                    LoadlistView(Showdata, take, Skip, ChoiceID, lblShowinfEntry, btnPrevious1, btnNext1, Listview1, ListView2, Totalrec, List);
                    redonlyfalse();
                }
                else
                {

                    List<TBLCOMPANYSETUP> List = DB.TBLCOMPANYSETUPs.Where(p => p.TenentID == TID && p.Active == "Y").ToList();
                    Countt = List.Count();
                    if (drpSort.SelectedValue == "1")
                        List = List.OrderBy(m => m.COMPNAME1).Where(p => p.Active == "Y" && p.TenentID == TID).ToList();//
                    if (drpSort.SelectedValue == "2")
                        List = List.OrderBy(m => m.Keyword).Where(p => p.Active == "Y" && p.TenentID == TID).ToList();//p.Active == "Y" &&
                    if (drpSort.SelectedValue == "3")
                        List = List.OrderBy(m => m.MOBPHONE).Where(p => p.Active == "Y" && p.TenentID == TID).ToList();//p.Active == "Y" &&
                    if (drpSort.SelectedValue == "4")
                        List = List.OrderBy(m => m.STATE).Where(p => p.Active == "Y" && p.TenentID == TID).ToList();//p.Active == "Y" &&
                    if (drpSort.SelectedValue == "5")
                        List = List.OrderBy(m => m.CITY).Where(p => p.Active == "Y" && p.TenentID == TID).ToList();//p.Active == "Y" &&
                    //if (drpSort.SelectedValue == "6")
                    //    List = List.OrderBy(m => m.ZIPCODE).Where(p => p.Active == "Y" && p.TenentID == TID).ToList();//p.Active == "Y" &&
                    //if (drpSort.SelectedValue == "7")
                    //    List = List.OrderBy(m => m.CITY).Where(p => p.Active == "Y" && p.TenentID == TID).ToList();//p.Active == "Y" &&
                    //if (drpSort.SelectedValue == "8")
                    //    List = List.OrderBy(m => m.REMARKS).Where(p => p.Active == "Y" && p.TenentID == TID).ToList();//p.Active == "Y" &&
                    int COMPID = List[0].COMPID;
                    int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
                    int Totalrec = Countt;

                    Loadlist(Showdata, take, Skip, ChoiceID, lblShowinfEntry, btnPrevious1, btnNext1, Listview1, ListView2, Totalrec, List);
                    BindEditcompniy(COMPID);
                    redonlyfalse();
                }

            }
            else
            {
                if (ViewState["SearchView"] != null)
                {
                    List<Database.View_CompanyMaster> List = ((List<Database.View_CompanyMaster>)ViewState["SearchView"]).ToList();
                    Countt = List.Count();
                    if (drpSort.SelectedValue == "1")
                        List = List.OrderBy(m => m.COMPNAME1).Where(p => p.TenentID == TID).ToList();//p.Active == "Y" &&
                    if (drpSort.SelectedValue == "2")
                        List = List.OrderBy(m => m.Keyword).Where(p => p.TenentID == TID).ToList();//p.Active == "Y" &&
                    if (drpSort.SelectedValue == "3")
                        List = List.OrderBy(m => m.MOBPHONE).Where(p => p.TenentID == TID).ToList();//p.Active == "Y" &&
                    if (drpSort.SelectedValue == "4")
                        List = List.OrderBy(m => m.MYNAME1).Where(p => p.TenentID == TID).ToList();//p.Active == "Y" &&
                    if (drpSort.SelectedValue == "5")
                        List = List.OrderBy(m => m.CityEnglish).Where(p => p.TenentID == TID).ToList();//p.Active == "Y" &&
                    //if (drpSort.SelectedValue == "6")
                    //    List = List.OrderBy(m => m.ZIPCODE).Where(p => p.TenentID == TID).ToList();//p.Active == "Y" &&
                    //if (drpSort.SelectedValue == "7")
                    //    List = List.OrderBy(m => m.CityEnglish).Where(p => p.TenentID == TID).ToList();//p.Active == "Y" &&
                    //if (drpSort.SelectedValue == "8")
                    //    List = List.OrderBy(m => m.Keyword).Where(p => p.TenentID == TID).ToList();//p.Active == "Y" &&
                    int COMPID = List[0].COMPID;
                    int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
                    int Totalrec = Countt;

                    ViewState["SearchView"] = List;
                    BindEditcompniy(COMPID);
                    LoadlistView(Showdata, take, Skip, ChoiceID, lblShowinfEntry, btnPrevious1, btnNext1, Listview1, ListView2, Totalrec, List);
                    redonlyfalse();
                }
                else
                {
                    List<TBLCOMPANYSETUP> List = DB.TBLCOMPANYSETUPs.Where(p => p.TenentID == TID && p.Active == "Y").ToList();
                    Countt = List.Count();
                    if (drpSort.SelectedValue == "1")
                        List = List.OrderBy(m => m.COMPNAME1).Where(p => p.TenentID == TID).ToList();//p.Active == "Y" &&
                    if (drpSort.SelectedValue == "2")
                        List = List.OrderBy(m => m.Keyword).Where(p => p.TenentID == TID).ToList();//p.Active == "Y" &&
                    if (drpSort.SelectedValue == "3")
                        List = List.OrderBy(m => m.MOBPHONE).Where(p => p.TenentID == TID).ToList();//p.Active == "Y" &&
                    if (drpSort.SelectedValue == "4")
                        List = List.OrderBy(m => m.STATE).Where(p => p.TenentID == TID).ToList();//p.Active == "Y" &&
                    if (drpSort.SelectedValue == "5")
                        List = List.OrderBy(m => m.CITY).Where(p => p.TenentID == TID).ToList();//p.Active == "Y" &&
                    //if (drpSort.SelectedValue == "6")
                    //    List = List.OrderBy(m => m.ZIPCODE).Where(p => p.TenentID == TID).ToList();//p.Active == "Y" &&
                    //if (drpSort.SelectedValue == "7")
                    //    List = List.OrderBy(m => m.CITY).Where(p => p.TenentID == TID).ToList();//p.Active == "Y" &&
                    //if (drpSort.SelectedValue == "8")
                    //    List = List.OrderBy(m => m.REMARKS).Where(p => p.TenentID == TID).ToList();//p.Active == "Y" &&
                    int COMPID = List[0].COMPID;
                    int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
                    int Totalrec = Countt;

                    BindEditcompniy(COMPID);
                    Loadlist(Showdata, take, Skip, ChoiceID, lblShowinfEntry, btnPrevious1, btnNext1, Listview1, ListView2, Totalrec, List);
                    redonlyfalse();
                }

            }

        }

        protected void btnNext1_Click(object sender, EventArgs e)
        {

            List<Database.TBLCOMPANYSETUP> Titel = new List<Database.TBLCOMPANYSETUP>();
            if (ViewState["Title"] != null)
            {
                Titel = ((List<Database.TBLCOMPANYSETUP>)ViewState["Title"]).ToList();
            }

            int Countt = 0;
            List<Database.View_CompanyMaster> VLIST = new List<Database.View_CompanyMaster>();
            if (ViewState["SearchView"] != null)
            {
                VLIST = ((List<Database.View_CompanyMaster>)ViewState["SearchView"]).ToList();
                Countt = VLIST.Count();
            }
            else
            {
                Countt = DB.TBLCOMPANYSETUPs.Where(p => p.TenentID == TID).Count();
            }

            if (Countt != 0)
            {

                int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
                int Totalrec = Countt;
                if (ViewState["Take"] == null && ViewState["Skip"] == null)
                {
                    ViewState["Take"] = Showdata;
                    ViewState["Skip"] = 0;
                }
                take = Convert.ToInt32(ViewState["Take"]);
                take = take + Showdata;
                Skip = take - Showdata;

                if (ViewState["SearchView"] != null)
                {
                    BindListView(VLIST.Take(take).Skip(Skip).ToList());
                }
                else
                {
                    if (Convert.ToInt32(DrpTitle.SelectedValue) != 999999)
                    {
                        BindList(Titel.Take(take).Skip(Skip).ToList());
                    }
                    else
                    {
                        BindList(DB.TBLCOMPANYSETUPs.Where(p => p.TenentID == TID).OrderByDescending(a => a.COMPID).Take(take).Skip(Skip).ToList());
                    }

                }

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
                GetCurrentNavigation(ChoiceID, Showdata, ListView2, Totalrec);
                lblShowinfEntry.Text = "Showing " + Skip + " to " + take + " of " + Totalrec + " entries";

                ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "please wait... ", "BlockStop()", true);
            }
        }
        protected void btnPrevious1_Click(object sender, EventArgs e)
        {
            List<Database.TBLCOMPANYSETUP> Titel = new List<Database.TBLCOMPANYSETUP>();
            if (ViewState["Title"] != null)
            {
                Titel = ((List<Database.TBLCOMPANYSETUP>)ViewState["Title"]).ToList();
            }
            int Countt = 0;
            List<Database.View_CompanyMaster> VLIST = new List<Database.View_CompanyMaster>();
            if (ViewState["SearchView"] != null)
            {
                VLIST = ((List<Database.View_CompanyMaster>)ViewState["SearchView"]).ToList();
                Countt = VLIST.Count();
            }
            else
            {
                Countt = DB.TBLCOMPANYSETUPs.Where(p => p.TenentID == TID).Count();
            }

            if (Countt != 0)
            {

                int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
                if (ViewState["Take"] != null && ViewState["Skip"] != null)
                {
                    int Totalrec = Countt;
                    Skip = Convert.ToInt32(ViewState["Skip"]);
                    take = Skip;
                    Skip = take - Showdata;

                    if (ViewState["SearchView"] != null)
                    {
                        BindListView(VLIST.Take(take).Skip(Skip).ToList());
                    }
                    else
                    {
                        if (Convert.ToInt32(DrpTitle.SelectedValue) != 999999)
                        {
                            BindList(Titel.Take(take).Skip(Skip).ToList());
                        }
                        else
                        {
                            BindList(DB.TBLCOMPANYSETUPs.Where(p => p.TenentID == TID).OrderByDescending(a => a.COMPID).Take(take).Skip(Skip).ToList());
                        }

                    }
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
                    GetCurrentNavigation(ChoiceID, Showdata, ListView2, Totalrec);
                    lblShowinfEntry.Text = "Showing " + Skip + " to " + take + " of " + Totalrec + " entries";

                    ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "please wait... ", "BlockStop()", true);
                }
            }
        }
        protected void btnfirst_Click(object sender, EventArgs e)
        {

            //int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
            //if (ViewState["Take"] != null && ViewState["Skip"] != null)
            //{
            //    int Totalrec = DB.TBLCOMPANYSETUPs.Count();
            //    take = Showdata;
            //    Skip = 0;
            //   BindList( (DB.TBLCOMPANYSETUPs.OrderBy(m => m.COMPID).Take(take).Skip(Skip)).ToList());
            //    ViewState["Take"] = take;
            //    ViewState["Skip"] = Skip;
            //    btnPrevious1.Enabled = false;
            //    ChoiceID = 0;
            //    GetCurrentNavigation(ChoiceID, Showdata, ListView2, Totalrec);
            //    if (take == Totalrec && Skip == (Totalrec - Showdata))
            //        btnNext1.Enabled = false;
            //    else
            //        btnNext1.Enabled = true;
            //    lblShowinfEntry.Text = "Showing " + Skip + " to " + take + " of " + Totalrec + " entries";

            //}
        }
        protected void btnfirst1_Click(object sender, EventArgs e)
        {
            List<Database.TBLCOMPANYSETUP> Titel = new List<Database.TBLCOMPANYSETUP>();
            if (ViewState["Title"] != null)
            {
                Titel = ((List<Database.TBLCOMPANYSETUP>)ViewState["Title"]).ToList();
            }
            int Countt = 0;
            List<Database.View_CompanyMaster> VLIST = new List<Database.View_CompanyMaster>();
            if (ViewState["SearchView"] != null)
            {
                VLIST = ((List<Database.View_CompanyMaster>)ViewState["SearchView"]).ToList();
                Countt = VLIST.Count();
            }
            else
            {
                Countt = DB.TBLCOMPANYSETUPs.Where(p => p.TenentID == TID).Count();
            }

            if (Countt != 0)
            {

                int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
                if (ViewState["Take"] != null && ViewState["Skip"] != null)
                {
                    int Totalrec = Countt;
                    take = Showdata;
                    Skip = 0;
                    if (ViewState["SearchView"] != null)
                    {
                        BindListView(VLIST.Take(take).Skip(Skip).ToList());
                    }
                    else
                    {
                        if (Convert.ToInt32(DrpTitle.SelectedValue) != 999999)
                        {
                            BindList(Titel.Take(take).Skip(Skip).ToList());
                        }
                        else
                        {
                            BindList(DB.TBLCOMPANYSETUPs.Where(p => p.TenentID == TID).OrderByDescending(a => a.COMPID).Take(take).Skip(Skip).ToList());
                        }
                    }

                    ViewState["Take"] = take;
                    ViewState["Skip"] = Skip;
                    btnPrevious1.Enabled = false;
                    ChoiceID = 0;
                    GetCurrentNavigation(ChoiceID, Showdata, ListView2, Totalrec);
                    if (take == Totalrec && Skip == (Totalrec - Showdata))
                        btnNext1.Enabled = false;
                    else
                        btnNext1.Enabled = true;
                    lblShowinfEntry.Text = "Showing " + Skip + " to " + take + " of " + Totalrec + " entries";

                    ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "please wait... ", "BlockStop()", true);
                }
            }
        }
        protected void btnLast1_Click(object sender, EventArgs e)
        {
            List<Database.TBLCOMPANYSETUP> Titel = new List<Database.TBLCOMPANYSETUP>();
            if (ViewState["Title"] != null)
            {
                Titel = ((List<Database.TBLCOMPANYSETUP>)ViewState["Title"]).ToList();
            }
            int Countt = 0;
            List<Database.View_CompanyMaster> VLIST = new List<Database.View_CompanyMaster>();
            if (ViewState["SearchView"] != null)
            {
                VLIST = ((List<Database.View_CompanyMaster>)ViewState["SearchView"]).ToList();
                Countt = VLIST.Count();
            }
            else
            {
                Countt = DB.TBLCOMPANYSETUPs.Where(p => p.TenentID == TID).Count();
            }
            if (Countt != 0)
            {

                int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
                int Totalrec = Countt;
                take = Totalrec;
                Skip = Totalrec - Showdata;
                if (ViewState["SearchView"] != null)
                {
                    BindListView(VLIST.Take(take).Skip(Skip).ToList());
                }
                else
                {
                    if (Convert.ToInt32(DrpTitle.SelectedValue) != 999999)
                    {
                        BindList(Titel.Take(take).Skip(Skip).ToList());
                    }
                    else
                    {
                        BindList(DB.TBLCOMPANYSETUPs.Where(p => p.TenentID == TID).OrderByDescending(a => a.COMPID).Take(take).Skip(Skip).ToList());
                    }
                }
                ViewState["Take"] = take;
                ViewState["Skip"] = Skip;
                btnNext1.Enabled = false;
                btnPrevious1.Enabled = true;
                ChoiceID = take / Showdata;
                GetCurrentNavigationLast(ChoiceID, Showdata, ListView2, Totalrec);
                lblShowinfEntry.Text = "Showing " + ViewState["Skip"].ToString() + " to " + ViewState["Take"].ToString() + " of " + Totalrec + " entries";

                ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "please wait... ", "BlockStop()", true);
            }
        }
        protected void ListView2_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            List<Database.TBLCOMPANYSETUP> Titel = new List<Database.TBLCOMPANYSETUP>();
            if (ViewState["Title"] != null)
            {
                Titel = ((List<Database.TBLCOMPANYSETUP>)ViewState["Title"]).ToList();
            }
            int Countt = 0;
            List<Database.View_CompanyMaster> VLIST = new List<Database.View_CompanyMaster>();
            if (ViewState["SearchView"] != null)
            {
                VLIST = ((List<Database.View_CompanyMaster>)ViewState["SearchView"]).ToList();
                Countt = VLIST.Count();
            }
            else
            {
                Countt = DB.TBLCOMPANYSETUPs.Where(p => p.TenentID == TID).Count();
            }

            if (Countt != 0)
            {

                int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
                int Totalrec = Countt;
                if (e.CommandName == "LinkPageavigation")
                {
                    int ID = Convert.ToInt32(e.CommandArgument);
                    ViewState["Take"] = ID * Showdata;
                    ViewState["Skip"] = (ID * Showdata) - Showdata;
                    int Tvalue = Convert.ToInt32(ViewState["Take"]);
                    int Svalue = Convert.ToInt32(ViewState["Skip"]);

                    if (ViewState["SearchView"] != null)
                    {
                        BindListView(VLIST.Take(Tvalue).Skip(Svalue).ToList());
                    }
                    else
                    {
                        if (Convert.ToInt32(DrpTitle.SelectedValue) != 999999)
                        {
                            BindList(Titel.Take(Tvalue).Skip(Svalue).ToList());
                        }
                        else
                        {
                            BindList(DB.TBLCOMPANYSETUPs.Where(p => p.TenentID == TID).OrderByDescending(a => a.COMPID).Take(Tvalue).Skip(Svalue).ToList());
                        }
                    }
                    ChoiceID = ID;
                    GetCurrentNavigation(ChoiceID, Showdata, ListView2, Totalrec);
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


                ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "please wait... ", "BlockStop()", true);
            }
        }
        protected void AnswerList_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            LinkButton lb = e.Item.FindControl("LinkPageavigation") as LinkButton;
            ScriptManager control = this.Master.FindControl("toolscriptmanagerID") as ScriptManager;
            control.RegisterAsyncPostBackControl(lb);  // ToolkitScriptManager
        }
        protected void LinkButton10_Click(object sender, EventArgs e)
        {


            if (txtSearch.Text != "")
            {
                string id1 = txtSearch.Text;
                int SelectSearch = Convert.ToInt32(drpSelectSearch.SelectedValue);
                if (SelectSearch == 0)
                {
                    //string Str = "";
                    //Str += "select * from TBLCOMPANYSETUP where TenentID=" + TID + "and (COMPNAME1 like '%" + id1 + "%' or COMPNAME2 like '%" + id1 + "%' or COMPNAME3 like '%" + id1 + "%' or MOBPHONE like '%" + id1 + "%';";
                    //command2 = new SqlCommand(Str, con);
                    //con.Open();
                    //command2.ExecuteReader();
                    //var List = Str;
                    //con.Close();

                    var List = DB.View_CompanyMaster.Where(p => (p.COMPNAME1.ToUpper().Contains(id1.ToUpper()) || p.COMPNAME2.ToUpper().Contains(id1.ToUpper()) || p.COMPNAME3.ToUpper().Contains(id1.ToUpper()) || p.Keyword.ToUpper().Contains(id1.ToUpper()) || p.ADDR1.ToUpper().Contains(id1.ToUpper()) || p.ADDR2.ToUpper().Contains(id1.ToUpper()) || p.MOBPHONE.ToUpper().Contains(id1.ToUpper()) || p.BUSPHONE1.ToUpper().Contains(id1.ToUpper()) || p.REMARKS.ToUpper().Contains(id1.ToUpper()) || p.CityEnglish.ToUpper().Contains(id1.ToUpper()) || p.MYNAME1.ToUpper().Contains(id1.ToUpper()) || p.EMAIL1.ToUpper().Contains(id1.ToUpper())) && p.TenentID == TID).OrderBy(p => p.COMPID).ToList();
                    ViewState["SearchView"] = List;
                    int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
                    int Totalrec = List.Count();

                    if (List.Count() > 0)
                    {
                        int CVID = Convert.ToInt32(List[0].COMPID);
                        BindEditcompniy(CVID);
                    }
                    LoadlistView(Showdata, take, Skip, ChoiceID, lblShowinfEntry, btnPrevious1, btnNext1, Listview1, ListView2, Totalrec, List);
                }
                else if (SelectSearch == 1)
                {
                    var List = DB.View_CompanyMaster.Where(p => (p.COMPNAME1.ToUpper().Contains(id1.ToUpper())) && p.TenentID == TID).OrderByDescending(p => p.COMPID).ToList();
                    ViewState["SearchView"] = List;
                    int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
                    int Totalrec = List.Count();

                    if (List.Count() > 0)
                    {
                        int CVID = Convert.ToInt32(List[0].COMPID);
                        BindEditcompniy(CVID);
                    }
                    LoadlistView(Showdata, take, Skip, ChoiceID, lblShowinfEntry, btnPrevious1, btnNext1, Listview1, ListView2, Totalrec, List);
                }
                else if (SelectSearch == 2)
                {
                    var List = DB.View_CompanyMaster.Where(p => (p.COMPNAME2.ToUpper().Contains(id1.ToUpper())) && p.TenentID == TID).OrderByDescending(p => p.COMPID).ToList();
                    ViewState["SearchView"] = List;
                    int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
                    int Totalrec = List.Count();

                    if (List.Count() > 0)
                    {
                        int CVID = Convert.ToInt32(List[0].COMPID);
                        BindEditcompniy(CVID);
                    }
                    LoadlistView(Showdata, take, Skip, ChoiceID, lblShowinfEntry, btnPrevious1, btnNext1, Listview1, ListView2, Totalrec, List);
                }
                else if (SelectSearch == 3)
                {
                    var List = DB.View_CompanyMaster.Where(p => (p.COMPNAME3.ToUpper().Contains(id1.ToUpper())) && p.TenentID == TID).OrderByDescending(p => p.COMPID).ToList();
                    ViewState["SearchView"] = List;
                    int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
                    int Totalrec = List.Count();

                    if (List.Count() > 0)
                    {
                        int CVID = Convert.ToInt32(List[0].COMPID);
                        BindEditcompniy(CVID);
                    }
                    LoadlistView(Showdata, take, Skip, ChoiceID, lblShowinfEntry, btnPrevious1, btnNext1, Listview1, ListView2, Totalrec, List);
                }
                else if (SelectSearch == 4)
                {
                    var List = DB.View_CompanyMaster.Where(p => (p.ADDR1.ToUpper().Contains(id1.ToUpper())) && p.TenentID == TID).OrderByDescending(p => p.COMPID).ToList();
                    ViewState["SearchView"] = List;
                    int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
                    int Totalrec = List.Count();

                    if (List.Count() > 0)
                    {
                        int CVID = Convert.ToInt32(List[0].COMPID);
                        BindEditcompniy(CVID);
                    }
                    LoadlistView(Showdata, take, Skip, ChoiceID, lblShowinfEntry, btnPrevious1, btnNext1, Listview1, ListView2, Totalrec, List);
                }
                else if (SelectSearch == 5)
                {
                    var List = DB.View_CompanyMaster.Where(p => (p.ADDR2.ToUpper().Contains(id1.ToUpper())) && p.TenentID == TID).OrderByDescending(p => p.COMPID).ToList();
                    ViewState["SearchView"] = List;
                    int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
                    int Totalrec = List.Count();

                    if (List.Count() > 0)
                    {
                        int CVID = Convert.ToInt32(List[0].COMPID);
                        BindEditcompniy(CVID);
                    }
                    LoadlistView(Showdata, take, Skip, ChoiceID, lblShowinfEntry, btnPrevious1, btnNext1, Listview1, ListView2, Totalrec, List);
                }
                else if (SelectSearch == 6)
                {
                    var List = DB.View_CompanyMaster.Where(p => (p.Keyword.ToUpper().Contains(id1.ToUpper())) && p.TenentID == TID).OrderByDescending(p => p.COMPID).ToList();
                    ViewState["SearchView"] = List;
                    int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
                    int Totalrec = List.Count();

                    if (List.Count() > 0)
                    {
                        int CVID = Convert.ToInt32(List[0].COMPID);
                        BindEditcompniy(CVID);
                    }
                    LoadlistView(Showdata, take, Skip, ChoiceID, lblShowinfEntry, btnPrevious1, btnNext1, Listview1, ListView2, Totalrec, List);
                }
                else if (SelectSearch == 7)
                {
                    var List = DB.View_CompanyMaster.Where(p => (p.CityEnglish.ToUpper().Contains(id1.ToUpper())) && p.TenentID == TID).OrderByDescending(p => p.COMPID).ToList();
                    ViewState["SearchView"] = List;
                    int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
                    int Totalrec = List.Count();

                    if (List.Count() > 0)
                    {
                        int CVID = Convert.ToInt32(List[0].COMPID);
                        BindEditcompniy(CVID);
                    }
                    LoadlistView(Showdata, take, Skip, ChoiceID, lblShowinfEntry, btnPrevious1, btnNext1, Listview1, ListView2, Totalrec, List);
                }
                else if (SelectSearch == 8)
                {

                    var List = DB.View_CompanyMaster.Where(p => (p.MYNAME1.ToUpper().Contains(id1.ToUpper())) && p.TenentID == TID).OrderByDescending(p => p.COMPID).ToList();
                    ViewState["SearchView"] = List;
                    int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
                    int Totalrec = List.Count();

                    if (List.Count() > 0)
                    {
                        int CVID = Convert.ToInt32(List[0].COMPID);
                        BindEditcompniy(CVID);
                    }
                    LoadlistView(Showdata, take, Skip, ChoiceID, lblShowinfEntry, btnPrevious1, btnNext1, Listview1, ListView2, Totalrec, List);
                }
                else if (SelectSearch == 9)
                {
                    var List = DB.View_CompanyMaster.Where(p => (p.MOBPHONE.ToUpper().Contains(id1.ToUpper())) && p.TenentID == TID).OrderByDescending(p => p.COMPID).ToList();
                    ViewState["SearchView"] = List;
                    int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
                    int Totalrec = List.Count();

                    if (List.Count() > 0)
                    {
                        int CVID = Convert.ToInt32(List[0].COMPID);
                        BindEditcompniy(CVID);
                    }
                    LoadlistView(Showdata, take, Skip, ChoiceID, lblShowinfEntry, btnPrevious1, btnNext1, Listview1, ListView2, Totalrec, List);
                }
                else if (SelectSearch == 10)
                {
                    var List = DB.View_CompanyMaster.Where(p => (p.BUSPHONE1.ToUpper().Contains(id1.ToUpper())) && p.TenentID == TID).OrderByDescending(p => p.COMPID).ToList();
                    ViewState["SearchView"] = List;
                    int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
                    int Totalrec = List.Count();

                    if (List.Count() > 0)
                    {
                        int CVID = Convert.ToInt32(List[0].COMPID);
                        BindEditcompniy(CVID);
                    }
                    LoadlistView(Showdata, take, Skip, ChoiceID, lblShowinfEntry, btnPrevious1, btnNext1, Listview1, ListView2, Totalrec, List);
                }
                else
                {
                    string msg = "Search Not Found...";
                    string Function = "openModal('" + msg + "');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", Function, true);
                }
            }
            else
            {
                string msg = "Please Fill Text In search You Want to Search...";
                string Function = "openModal('" + msg + "');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", Function, true);
            }
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
            if (Listview1.Items.Count > 0)
            {
                Listview1.SelectedIndex = 0;
                if (Listview1.SelectedIndex == 0)
                {

                    int LIDID = Convert.ToInt32(Listview1.SelectedDataKey[0]);
                    //int LIDID = DB.TBLCOMPANYSETUPs.Where(p => p.TenentID == TID).FirstOrDefault().COMPID;
                    //Listview1.SelectedIndex = LIDID;
                    BindEditcompniy(LIDID);
                    redonlyfalse();
                    string Cname = GetCustomerName(LIDID);
                    lblBusContactDe.Text = "Business Company Details -  (View Mode) - " + Cname;
                    lblBCDeta.Text = "Business Company Details -  (View Mode)- " + Cname;
                    lblwExistance.Text = "Web Existance -  (View Mode)- " + Cname;
                    lblWEmp.Text = "Working Employees -  (View Mode)- " + Cname;

                }
            }
        }

        protected void btnNext_Click(object sender, EventArgs e)
        {
            if (Listview1.Items.Count > 0)
            {
                if (Listview1.SelectedIndex != Listview1.Items.Count - 1)
                {
                    Listview1.SelectedIndex = Listview1.SelectedIndex + 1;
                    int LIDID = Convert.ToInt32(Listview1.SelectedDataKey[0]);
                    //Listview1.SelectedIndex = Listview1.SelectedIndex;
                    //BindEditcompniy(Listview1.SelectedIndex);
                    BindEditcompniy(LIDID);
                    redonlyfalse();
                    string Cname = GetCustomerName(LIDID);
                    lblBusContactDe.Text = "Business Company Details -  (View Mode) - " + Cname;
                    lblBCDeta.Text = "Business Company Details -  (View Mode)- " + Cname;
                    lblwExistance.Text = "Web Existance -  (View Mode)- " + Cname;
                    lblWEmp.Text = "Working Employees -  (View Mode)- " + Cname;

                }
            }
        }

        protected void btnPrev_Click(object sender, EventArgs e)
        {
            if (Listview1.Items.Count > 0)
            {
                if (Listview1.SelectedIndex == 0 && Listview1.SelectedIndex < 0)
                {
                    //lblMsg.Text = "This is first record";
                    //pnlSuccessMsg.Visible = true;

                }
                else
                {
                    Listview1.SelectedIndex = Listview1.SelectedIndex - 1;

                    if (Listview1.SelectedIndex == 0)
                    {
                        Listview1.SelectedIndex = 1;
                    }
                    //int LIDID = Convert.ToInt32(Listview1.SelectedDataKey[0]);
                    //    LIDID = Convert.ToInt32(Listview1.SelectedIndex);

                    //Listview1.SelectedIndex = Listview1.SelectedIndex;
                    int LIDID = Convert.ToInt32(Listview1.SelectedDataKey[0]);
                    BindEditcompniy(LIDID);
                    redonlyfalse();
                    string Cname = GetCustomerName(LIDID);
                    lblBusContactDe.Text = "Business Company Details -  (View Mode) - " + Cname;
                    lblBCDeta.Text = "Business Company Details -  (View Mode)- " + Cname;
                    lblwExistance.Text = "Web Existance -  (View Mode)- " + Cname;
                    lblWEmp.Text = "Working Employees -  (View Mode)- " + Cname;

                }
            }
        }

        protected void btnLast_Click(object sender, EventArgs e)
        {
            if (Listview1.Items.Count > 0)
            {
                Listview1.SelectedIndex = Listview1.Items.Count - 1;
                int LIDID = Convert.ToInt32(Listview1.SelectedDataKey[0]); // DB.TBLCOMPANYSETUPs.Where(p => p.Active == "Y" && p.TenentID == TID).Max(p => p.COMPID);

                BindEditcompniy(LIDID);
                redonlyfalse();
                string Cname = GetCustomerName(LIDID);
                lblBusContactDe.Text = "Business Company Details -  (View Mode) - " + Cname;
                lblBCDeta.Text = "Business Company Details -  (View Mode)- " + Cname;
                lblwExistance.Text = "Web Existance -  (View Mode)- " + Cname;
                lblWEmp.Text = "Working Employees -  (View Mode)- " + Cname;
                //Listview1.SelectedIndex = Listview1.SelectedIndex;

                //int COMID = DB.TBLCOMPANYSETUP.Where(p => p.Active == "Y").Max(p => p.COMPID);
                //BindEditcompniy(COMID);
                //redonlyfalse();
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            ViewState["compId"] = null;
            redonlyture();
            btnSubmit.Visible = true;
            btnSubmit.Text = "Save";
            txtCustoID.Text = DB.TBLCOMPANYSETUPs.Where(p => p.TenentID == TID).Count() > 0 ? (DB.TBLCOMPANYSETUPs.Where(p => p.TenentID == TID).Max(p => p.COMPID) + 1).ToString() : "1";
            txtCustoID.Enabled = true;
            clen();

            int LEN = txtCustoID.Text.Length;
            if (LEN < 4)
                txtBarCode.Text = "Bar" + txtCustoID.Text;
            else
                txtBarCode.Text = txtCustoID.Text;
            chbBuyer.Checked = true;
            drpPrimaryLang.SelectedValue = "1";
            Button1.Visible = false;
            if (DB.MYCOMPANYSETUPs.Where(p => p.TenentID == TID && p.COUNTRYID != null).Count() > 0)
            {
                //drpCountry.SelectedValue = DB.MYCOMPANYSETUPs.Single(p => p.TenentID == TID && p.COUNTRYID != null).COUNTRYID.ToString();
                string CID = DB.MYCOMPANYSETUPs.FirstOrDefault(p => p.TenentID == TID).COUNTRYID.ToString();
                drpCountry.SelectedValue = CID;
                bindSates(CID);
                drpSates.SelectedValue = "1902";
                int stateiid = Convert.ToInt32(drpSates.SelectedValue);
                int COUNTRYID = Convert.ToInt32(drpCountry.SelectedValue);
                drpcity.DataSource = DB.tblCityStatesCounties.Where(p => p.COUNTRYID == COUNTRYID && p.StateID == stateiid).OrderBy(p => p.CityEnglish);
                drpcity.DataTextField = "CityEnglish";
                drpcity.DataValueField = "CityID";
                drpcity.DataBind();
                drpcity.Items.Insert(0, new ListItem("-- Select --", "0"));
                drpcity.SelectedValue = "2";
                updatepaneldrp();
            }

            if (Request.QueryString["Type"] != null)
            {
                string Comptype = Request.QueryString["Type"].ToString();
                drpType.SelectedValue = Comptype;
                drpType.Enabled = false;
            }
            else
            {
                drpType.SelectedValue = "82003";
                drpType.Enabled = true;
            }
            if (txtBarCode.Text != "")
            {
                string barCode = txtBarCode.Text.Trim();
                string str = Server.MapPath("/CRM/images/" + barCode + ".png");
                System.Drawing.Image myimg = MakeBarcode(barCode, 4, true);//BarIMG

                if (!File.Exists(str))
                {
                    System.Web.UI.WebControls.Image imgBarCode = new System.Web.UI.WebControls.Image();
                    using (Bitmap bitMap = new Bitmap(barCode.Length * 40, 80))
                    {
                        using (MemoryStream ms = new MemoryStream())
                        {
                            myimg.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                            byte[] byteImage = ms.ToArray();

                            Convert.ToBase64String(byteImage);
                            imgBarCode.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(byteImage);
                            try
                            {
                                File.WriteAllBytes(Server.MapPath("/CRM/images/" + barCode + ".png"), byteImage);
                            }
                            catch (System.Exception ex)
                            {
                                Response.Write(ex.InnerException);
                            }
                        }
                    }
                }
                showBarcode(barCode);
            }
            Avatar.ImageUrl = "~/Gallery/defolt.png";
            btnSaveandConti.Visible = true;
            NullBullContact();
        }

        protected void btnAppend_Click(object sender, EventArgs e)
        {
            int ID = Convert.ToInt32(DrpTitle.SelectedValue);
            int Con = 0;
            if (ViewState["SearchView"] != null)
            {
                var Listserch = ((List<Database.View_CompanyMaster>)ViewState["SearchView"]).ToList();
                for (int i = 0; i < Listserch.Count; i++)
                {
                    int HID = Convert.ToInt32(Listserch[i].COMPID);
                    // Con = Convert.ToInt32(HID.Value);
                    Database.ISSearchDetail obj_Detail = new ISSearchDetail();
                    obj_Detail.ID = DB.ISSearchDetails.Where(p => p.TenentID == TID).Count() > 0 ? Convert.ToInt32(DB.ISSearchDetails.Where(p => p.TenentID == TID).Max(p => p.ID) + 1) : 1;
                    obj_Detail.TenentID = TID;
                    obj_Detail.LocationID = LID;
                    obj_Detail.REFID = ID;
                    obj_Detail.CompanyID = HID;
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
            else if (Listview1.Items.Count() > 0)
            {
                for (int j = 0; j < Listview1.Items.Count; j++)
                {
                    Label Label91 = (Label)Listview1.Items[j].FindControl("Label91");
                    int HID = Convert.ToInt32(Label91.Text);
                    Database.ISSearchDetail obj_Detail = new Database.ISSearchDetail();
                    obj_Detail.ID = DB.ISSearchDetails.Where(p => p.TenentID == TID).Count() > 0 ? Convert.ToInt32(DB.ISSearchDetails.Where(p => p.TenentID == TID).Max(p => p.ID) + 1) : 1;
                    obj_Detail.TenentID = TID;
                    obj_Detail.LocationID = LID;
                    obj_Detail.REFID = ID;
                    obj_Detail.CompanyID = HID;
                    obj_Detail.CreatedBy = UID;
                    obj_Detail.Active = true;
                    obj_Detail.Deleted = true;
                    if (DB.ISSearchDetails.Where(p => p.CompanyID == HID && p.CreatedBy == UID && p.REFID == ID && p.TenentID == TID).Count() < 1)
                    {
                        DB.ISSearchDetails.AddObject(obj_Detail);
                        DB.SaveChanges();
                        flag = true;
                    }
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
            List<TBLCONTACT> list1 = DB.TBLCONTACTs.Where(p => p.TenentID == TID && (p.PersName1.ToUpper().Contains(id1.ToUpper()) || p.EMAIL1.ToUpper().Contains(id1.ToUpper()) || p.ADDR1.ToUpper().Contains(id1.ToUpper()) || p.CITY.ToUpper().Contains(id1.ToUpper()) || p.STATE.ToUpper().Contains(id1.ToUpper()) || p.REMARKS.ToUpper().Contains(id1.ToUpper()) || p.MOBPHONE.ToUpper().Contains(id1.ToUpper())) && p.Active == "Y").OrderBy(p => p.PersName1).ToList();
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
            int CCID = 0;
            List.TenentID = TID;
            if (btnSubmit.Text == "Save")
                List.COMPID = DB.TBLCOMPANYSETUPs.Where(p => p.TenentID == TID).Count() > 0 ? Convert.ToInt32(DB.TBLCOMPANYSETUPs.Where(p => p.TenentID == TID).Max(p => p.COMPID) + 1) : 1;
            else
            {
                CCID = Convert.ToInt32(txtCustoID.Text);
                List.COMPID = CCID;
            }
            ViewState["CIDRF"] = Convert.ToInt32(List.COMPID);
            List.PHYSICALLOCID = drplocation.SelectedValue;
            List.COMPNAME1 = txtCustomerName.Text;
            List.COMPNAME2 = txtCustomer.Text;
            List.COMPNAME3 = txtCustomer2.Text;
            List.EMAIL1 = tags_2.Text;
            // objtbl_COMPANYSETUP.EMAIL2 = tags_2.Text;
            List.ITMANAGER = drpItManager.SelectedValue;
            List.ADDR1 = txtAddress.Text;
            List.ADDR2 = txtAddress2.Text;
            List.CivilID = txtCivilID.Text;
            if (txtBirthdate.Text != "")
            {
                List.BirthDate = Convert.ToDateTime(txtBirthdate.Text);
            }

            //List.CITY = txtCity.Text;
            List.CITY = drpcity.SelectedValue;
            List.STATE = drpSates.SelectedValue;
            List.POSTALCODE = txtPostalCode.Text;
            List.ZIPCODE = txtZipCode.Text;
            //List.MYCONLOCID = Convert.ToInt32(drpMyCounLocID.SelectedValue);
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
            List.Marketting = txtrefreshno.Text;
            List.CUSERID = txtcUserName.Text;
            List.CPASSWRD = txtcPassword.Text;
            List.USERID = UID;
            List.ENTRYDATE = DateTime.Now;
            List.ENTRYTIME = DateTime.Now;
            List.UPDTTIME = DateTime.Now;
            List.Approved = 0;
            ListTBLCOMPANYSETUP.Add(List);
            Session["ListTBLCOMPANYSETUP"] = ListTBLCOMPANYSETUP;
            if (btnSubmit.Text == "Save")
                Response.Redirect("ContactMaster.aspx?AddContect=New");
            else
                Response.Redirect("ContactMaster.aspx?AddContect=New&CCID=" + CCID);
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

            string tagdt;


            string drpval = drpCategory.SelectedItem.ToString();
            if (tags_1.Text != "")
            {
                tagdt = tags_1.Text;
                tagdt += "," + drpval;
                tags_1.Text = tagdt;
            }
            else
            {
                tagdt = drpval;
                tags_1.Text = tagdt;
            }
            ViewState["SaveList1"] = tags_1.Text;

        }

        protected void btnMinClassfication_Click(object sender, EventArgs e)
        {
            if (txtclassficname.Text != "")
            {
                if (DB.CAT_MST.Where(p => p.CAT_NAME1 == txtclassficname.Text && p.PARENT_CATID == 7861 && p.CAT_TYPE == "COMP" && p.TenentID == TID).Count() <= 1)
                {
                    Database.CAT_MST objcat = new Database.CAT_MST();
                    objcat.CATID = DB.CAT_MST.Where(p => p.TenentID == TID).Count() > 0 ? Convert.ToInt32(DB.CAT_MST.Where(p => p.TenentID == TID).Max(p => p.CATID) + 1) : 1;
                    objcat.TenentID = TID;
                    objcat.CAT_NAME1 = txtclassficname.Text;
                    objcat.CAT_NAME2 = txtclassficname.Text;
                    objcat.CAT_NAME3 = txtclassficname.Text;
                    objcat.CAT_DESCRIPTION = txtclassficname.Text;
                    //objcat.SHORT_NAME = txtclassficname.Text;
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
            }





            BindDataClassfic();
        }

        protected void drpCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            string CID = drpCountry.SelectedValue;
            bindSates(CID);
            int COUNTRYID = Convert.ToInt32(drpCountry.SelectedValue);
            drpcity.DataSource = DB.tblCityStatesCounties.Where(p => p.COUNTRYID == COUNTRYID).OrderBy(p => p.CityEnglish);
            drpcity.DataTextField = "CityEnglish";
            drpcity.DataValueField = "CityID";
            drpcity.DataBind();
            drpcity.Items.Insert(0, new ListItem("-- Select --", "0"));
            updatepaneldrp();
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



        protected void Listview1_ItemDataBound(object sender, ListViewItemEventArgs e)
        {


            //if (ListCompsetup.Count() < 1)
            //{
            //    ListCompsetup = DB.TBLCOMPANYSETUPs.Where(p => p.TenentID == TID).ToList();

            //}

            //LinkButton lnkbtnActive = (LinkButton)e.Item.FindControl("lnkbtnActive");
            //Label lblcompid = (Label)e.Item.FindControl("lblcompid");
            //int ID = Convert.ToInt32(lblcompid.Text);

            //if (ListCompsetup.Where(p => p.COMPID == ID && p.TenentID == TID && p.Active == "Y").Count() > 0)
            //{
            //    lnkbtnActive.Text = "DeActivate";
            //    //lnkbtnActive.CssClass = "btn btn-sm red filter-submit margin-bottom";
            //}
            //else
            //{
            //    lnkbtnActive.Text = "Active";
            //    //lnkbtnActive.CssClass = "btn btn-sm green filter-submit margin-bottom";
            //}
        }

        protected void drpSates_SelectedIndexChanged(object sender, EventArgs e)
        {
            int state = Convert.ToInt32(drpSates.SelectedValue);
            if (state != 0)
            {
                drpcity.DataSource = DB.tblCityStatesCounties.Where(p => p.StateID == state).OrderBy(p => p.CityEnglish);
                drpcity.DataTextField = "CityEnglish";
                drpcity.DataValueField = "CityID";
                drpcity.DataBind();
                drpcity.Items.Insert(0, new ListItem("-- Select --", "0"));
            }
            else
            {
                drpcity.DataSource = DB.tblCityStatesCounties.OrderBy(p => p.CityEnglish);
                drpcity.DataTextField = "CityEnglish";
                drpcity.DataValueField = "CityID";
                drpcity.DataBind();
                drpcity.Items.Insert(0, new ListItem("-- Select --", "0"));
            }
            updatepaneldrp();
        }

        protected void txtBirthdate_TextChanged(object sender, EventArgs e)
        {
            if (txtBirthdate.Text != "")
            {
                DateTime bday = Convert.ToDateTime(txtBirthdate.Text);
                DateTime today = DateTime.Now;
                if (bday <= today)
                { }
                else
                {
                    PanelError.Visible = true;
                    lblerror.Text = "Enter Birthday not greater than today";
                    return;
                }
            }
            else
            {
                PanelError.Visible = true;
                lblerror.Text = "Enter Valid Birthday";
                return;
            }


        }

        protected void chkactive_CheckedChanged(object sender, EventArgs e)
        {
            if (chkactive.Checked == true)
            {
                BindDataActive();
            }
            else
            {
                BindData();
            }
        }

        public string GetCustomerName(int CID)
        {
            List<Database.TBLCOMPANYSETUP> Listcompnay = DB.TBLCOMPANYSETUPs.Where(p => p.TenentID == TID && p.COMPID == CID).ToList();
            if (Listcompnay.Count() > 0)
            {
                return Listcompnay.Single(p => p.TenentID == TID && p.COMPID == CID).COMPNAME1;
            }
            else
            {
                return " Record Not Found";
            }
        }

        public void updatepaneldrp()
        {
            drpCountry.Attributes["class"] = drpSates.Attributes["class"] = drplocation.Attributes["class"] = drpcity.Attributes["class"] = "form-control select2me";
            updCompny.Update();
        }


        protected void drpcity_SelectedIndexChanged(object sender, EventArgs e)
        {
            int Country = Convert.ToInt32(drpCountry.SelectedValue);
            int City = Convert.ToInt32(drpcity.SelectedValue);

            if (Country != 0 && City != 0)
            {
                drpSates.SelectedValue = DB.tblCityStatesCounties.Where(p => p.COUNTRYID == Country && p.CityID == City).FirstOrDefault().StateID.ToString();
            }

            updatepaneldrp();
        }

        protected void btnSaveBrand_Click(object sender, EventArgs e)
        {
            int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            if (txtBrand.Text != "")
            {
                if (DB.REFTABLEs.Where(p => p.REFNAME1.ToLower() == txtBrand.Text.ToLower() && p.TenentID == TID && p.REFTYPE == "BRAND" && p.REFSUBTYPE == "OTH").Count() < 1)
                {
                    Database.REFTABLE REFOBJ = new Database.REFTABLE();
                    REFOBJ.TenentID = TID;
                    REFOBJ.REFID = DB.REFTABLEs.Where(p => p.TenentID == TID).Count() > 0 ? Convert.ToInt32(DB.REFTABLEs.Where(p => p.TenentID == TID).Max(p => p.REFID) + 1) : 1;
                    REFOBJ.REFTYPE = "BRAND";
                    REFOBJ.REFSUBTYPE = "OTH";
                    REFOBJ.SHORTNAME = txtBrand.Text;
                    REFOBJ.REFNAME1 = txtBrand.Text;
                    REFOBJ.REFNAME2 = txtBrand.Text;
                    REFOBJ.REFNAME3 = txtBrand.Text;
                    REFOBJ.SWITCH1 = "1";
                    REFOBJ.SWITCH2 = "2";
                    REFOBJ.SWITCH3 = "99";
                    REFOBJ.SWITCH4 = 0;
                    REFOBJ.Remarks = txtBrand.Text;
                    REFOBJ.ACTIVE = "Y";
                    DB.REFTABLEs.AddObject(REFOBJ);
                    DB.SaveChanges();
                    Classes.EcommAdminClass.getdropdown(drpbrand, TID, "BRAND", "OTH", "", "REFTABLE");
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('BRAND Is All Ready Exist...');", true);
                    return;
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Please Insert BRAND...');", true);
                return;
            }
        }

        protected void drpbrand_SelectedIndexChanged(object sender, EventArgs e)
        {
            string drpval = drpbrand.SelectedItem.ToString();
            string tagdt = "";

            if (tags_5.Text != "")
            {
                tagdt = tags_5.Text;
                tagdt += "," + drpval;
                tags_5.Text = tagdt;

            }
            else
            {
                tagdt = drpval;
                tags_5.Text = tagdt;

            }

            ViewState["SaveList2"] = tags_5.Text;
        }

        protected void txtCustoID_TextChanged(object sender, EventArgs e)
        {
            PanelError.Visible = false;
            lblerror.Text = "";
            if (txtCustoID.Text != "")
            {
                int CID = Convert.ToInt32(txtCustoID.Text);
                if (DB.TBLCOMPANYSETUPs.Where(p => p.TenentID == TID && p.COMPID == CID).Count() > 0)
                {
                    PanelError.Visible = true;
                    lblerror.Text = "Customer ID Already Exist";
                    return;
                }
            }
            else
            {
                PanelError.Visible = true;
                lblerror.Text = "Please Enter Customer ID";
                return;
            }

        }


        protected void Button3_Click(object sender, EventArgs e)
        {
            if (FileUpload1.HasFile)
            {
                FileUpload1.SaveAs(Server.MapPath("Upload/") + FileUpload1.FileName);
                string strFileName = Server.HtmlEncode(FileUpload1.FileName);
                string strExtension = Path.GetExtension(strFileName);
                if (strExtension != ".xls" && strExtension != ".xlsx")
                {
                    Response.Write("<script>alert('Please select a Excel spreadsheet to import!');</script>");
                    return;
                }
                string strUploadFileName = "Upload/" + strFileName;
                FileUpload1.SaveAs(Server.MapPath(strUploadFileName));
                string strExcelConn = "";
                strExcelConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Server.MapPath(strUploadFileName) + ";Extended Properties='Excel 12.0 Xml;HDR=YES;'";
                DataTable dtExcel = RetrieveData(strExcelConn);
                SqlBulkCopyImport1(dtExcel);
                File.Delete(Server.MapPath(strUploadFileName));

            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            List<Database.TBLCOMPANYSETUP_ExcelImport> List = DB.TBLCOMPANYSETUP_ExcelImport.Where(p => p.TenentID == TID).ToList();
            foreach (Database.TBLCOMPANYSETUP_ExcelImport item in List)
            {
                Database.TBLCOMPANYSETUP objimport = new Database.TBLCOMPANYSETUP();
                objimport.TenentID = item.TenentID;
                objimport.COMPID = DB.TBLCOMPANYSETUPs.Where(p => p.TenentID == TID).Count() > 0 ? Convert.ToInt32(DB.TBLCOMPANYSETUPs.Where(p => p.TenentID == TID).Max(p => p.COMPID) + 1) : 1;

                objimport.OldCOMPid = item.OldCOMPid;
                objimport.PHYSICALLOCID = item.PHYSICALLOCID;
                objimport.COMPNAME1 = item.COMPNAME1;
                objimport.COMPNAME2 = item.COMPNAME2;
                objimport.COMPNAME3 = item.COMPNAME3;
                objimport.BirthDate = item.BirthDate;
                objimport.CivilID = item.CivilID;
                objimport.EMAIL = item.EMAIL;
                objimport.EMAIL1 = item.EMAIL1;
                objimport.EMAIL2 = item.EMAIL2;
                objimport.ITMANAGER = item.ITMANAGER;
                objimport.ADDR1 = item.ADDR1;
                objimport.ADDR2 = item.ADDR2;
                objimport.CITY = item.CITY;
                objimport.STATE = item.STATE;
                objimport.POSTALCODE = item.POSTALCODE;
                objimport.ZIPCODE = item.ZIPCODE;
                objimport.MYCONLOCID = item.MYCONLOCID;
                objimport.MYPRODID = item.MYPRODID;
                objimport.COUNTRYID = item.COUNTRYID;
                objimport.BUSPHONE1 = item.BUSPHONE1;
                objimport.BUSPHONE2 = item.BUSPHONE2;
                objimport.BUSPHONE3 = item.BUSPHONE3;
                objimport.BUSPHONE4 = item.BUSPHONE4;
                objimport.MOBPHONE = item.MOBPHONE;
                objimport.FAX = item.FAX;
                objimport.FAX1 = item.FAX1;
                objimport.FAX2 = item.FAX2;
                objimport.PRIMLANGUGE = item.PRIMLANGUGE;
                objimport.WEBPAGE = item.WEBPAGE;
                objimport.ISMINISTRY = item.ISMINISTRY;
                objimport.ISSMB = item.ISSMB;
                objimport.ISCORPORATE = item.ISCORPORATE;
                objimport.INHAWALLY = item.INHAWALLY;
                objimport.SALER = item.SALER;
                objimport.BUYER = item.BUYER;
                objimport.SALEDEPROD = item.SALEDEPROD;
                objimport.EMAISUB = item.EMAISUB;
                objimport.EMAILSUBDATE = item.EMAILSUBDATE;
                objimport.PRODUCTDEALIN = item.PRODUCTDEALIN;
                objimport.REMARKS = item.REMARKS;
                objimport.Keyword = item.Keyword;
                objimport.COMPANYID = item.COMPANYID;
                objimport.BUSID = item.BUSID;
                objimport.MYCATSUBID = item.MYCATSUBID;
                objimport.COMPNAME = item.COMPNAME;
                objimport.COMPNAMEO = item.COMPNAMEO;
                objimport.COMPNAMECH = item.COMPNAMECH;
                objimport.Active = item.Active;
                objimport.CRUP_ID = item.CRUP_ID;
                objimport.CUSERID = item.CUSERID;
                objimport.CPASSWRD = item.CPASSWRD;
                objimport.USERID = item.USERID;
                objimport.ENTRYDATE = item.ENTRYDATE;
                objimport.ENTRYTIME = item.ENTRYTIME;
                objimport.UPDTTIME = item.UPDTTIME;
                objimport.Approved = item.Approved;
                objimport.CompanyType = item.CompanyType;
                objimport.Images = item.Images;
                objimport.BARCODE = item.BARCODE;
                objimport.Avtar = item.Avtar;
                objimport.Reload = item.Reload;
                objimport.datasource = item.datasource;
                DB.TBLCOMPANYSETUPs.AddObject(objimport);
                DB.SaveChanges();
            }
            foreach (Database.TBLCOMPANYSETUP_ExcelImport item in List)
            {
                DB.TBLCOMPANYSETUP_ExcelImport.DeleteObject(item);
                DB.SaveChanges();
            }
            GetList();
            Response.Write("<script>alert('Import Excel spreadsheet Successfull...');</script>");
            return;
        }

        protected void BtnDownload_Click(object sender, EventArgs e)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/xlsx";
            Response.AddHeader("content-disposition", "attachment;filename=Company_Formate.xlsx");
            Response.TransmitFile(Server.MapPath("Upload/Company_Formate.xlsx"));
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.End();
        }

        protected void ckbAll_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < Listview3.Items.Count; i++)
            {
                CheckBox chk = (CheckBox)Listview3.Items[i].FindControl("cheList");
                if (ckbAll.Checked == true)
                    chk.Checked = true;
                else
                    chk.Checked = false;
            }
        }

        protected DataTable RetrieveData(string strConn)
        {
            DataTable dtExcel = new DataTable();

            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                // Initialize an OleDbDataAdapter object.
                OleDbDataAdapter da = new OleDbDataAdapter("select * from [Sheet1$]", conn);

                // Fill the DataTable with data from the Excel spreadsheet.
                da.Fill(dtExcel);
            }
            return dtExcel;
        }
        protected void SqlBulkCopyImport1(DataTable dtExcel)
        {


            for (int i = 0; i < dtExcel.Rows.Count; i++)
            {
                int flag = 0;
                //TBLPRODUCT_MAINTANANCE obj = new TBLPRODUCT_MAINTANANCE();
                Database.TBLCOMPANYSETUP_ExcelImport obj = new Database.TBLCOMPANYSETUP_ExcelImport();

                obj.TenentID = TID;
                int COMPID1 = DB.TBLCOMPANYSETUP_ExcelImport.Where(p => p.TenentID == TID).Count() > 0 ? Convert.ToInt32(DB.TBLCOMPANYSETUP_ExcelImport.Where(p => p.TenentID == TID).Max(p => p.COMPID) + 1) : 1;
                obj.COMPID = COMPID1;
                obj.OldCOMPid = 0;
                obj.PHYSICALLOCID = "KWT";
                if (dtExcel.Rows[i]["COMPNAME1"].ToString().Trim() != null && dtExcel.Rows[i]["COMPNAME1"].ToString().Trim() != "")
                    obj.COMPNAME1 = dtExcel.Rows[i]["COMPNAME1"].ToString();
                else
                    obj.COMPNAME1 = "DEMO";

                if (dtExcel.Rows[i]["COMPNAME2"].ToString().Trim() != null && dtExcel.Rows[i]["COMPNAME2"].ToString().Trim() != "")
                    obj.COMPNAME2 = dtExcel.Rows[i]["COMPNAME2"].ToString();
                else
                    obj.COMPNAME2 = "DEMO2";

                if (dtExcel.Rows[i]["COMPNAME3"].ToString().Trim() != null && dtExcel.Rows[i]["COMPNAME3"].ToString().Trim() != "")
                    obj.COMPNAME3 = dtExcel.Rows[i]["COMPNAME3"].ToString();
                else
                    obj.COMPNAME3 = "DEMO3";

                obj.BirthDate = null;
                if (dtExcel.Rows[i]["CivilID"].ToString().Trim() != null && dtExcel.Rows[i]["CivilID"].ToString().Trim() != "")
                    obj.CivilID = dtExcel.Rows[i]["CivilID"].ToString();
                else
                    obj.CivilID = "123";

                if (dtExcel.Rows[i]["EMAIL"].ToString().Trim() != null && dtExcel.Rows[i]["EMAIL"].ToString().Trim() != "")
                    obj.EMAIL = dtExcel.Rows[i]["EMAIL"].ToString();
                else
                    obj.EMAIL = "Demo@demo.com";

                if (dtExcel.Rows[i]["EMAIL"].ToString().Trim() != null && dtExcel.Rows[i]["EMAIL"].ToString().Trim() != "")
                    obj.EMAIL1 = dtExcel.Rows[i]["EMAIL"].ToString();
                else
                    obj.EMAIL1 = "Demo@demo.com";

                if (dtExcel.Rows[i]["EMAIL"].ToString().Trim() != null && dtExcel.Rows[i]["EMAIL"].ToString().Trim() != "")
                    obj.EMAIL2 = dtExcel.Rows[i]["EMAIL"].ToString();
                else
                    obj.EMAIL2 = "Demo@demo.com";

                obj.ITMANAGER = "Not Found";

                if (dtExcel.Rows[i]["Address"].ToString().Trim() != null && dtExcel.Rows[i]["Address"].ToString().Trim() != "")
                    obj.ADDR1 = dtExcel.Rows[i]["Address"].ToString();
                else
                    obj.ADDR1 = "Kuwait";

                obj.ADDR2 = "Kuwait";

                int CID = 0;
                if (dtExcel.Rows[i]["Country"].ToString().Trim() != null && dtExcel.Rows[i]["Country"].ToString().Trim() != "")
                {
                    string CNAME = dtExcel.Rows[i]["Country"].ToString();
                    CID = DB.tblCOUNTRies.Where(p => p.TenentID == TID && p.COUNAME1.ToUpper().Contains(CNAME.ToUpper())).Select(p => p.COUNTRYID).FirstOrDefault();
                    obj.COUNTRYID = CID;
                }
                else
                {
                    obj.COUNTRYID = 126;
                }

                int SID = 0;
                if (dtExcel.Rows[i]["State"].ToString().Trim() != null && dtExcel.Rows[i]["State"].ToString().Trim() != "")
                {
                    string CNAME = dtExcel.Rows[i]["State"].ToString();
                    SID = DB.tblStates.Where(p => p.COUNTRYID == CID && p.MYNAME1.ToUpper().Contains(CNAME.ToUpper())).Select(p => p.StateID).FirstOrDefault();
                    obj.STATE = SID.ToString();
                }
                else
                {
                    obj.STATE = "1906";
                }

                if (dtExcel.Rows[i]["City"].ToString().Trim() != null && dtExcel.Rows[i]["City"].ToString().Trim() != "")
                {
                    string CNAME = dtExcel.Rows[i]["City"].ToString();
                    int CityID = DB.tblCityStatesCounties.Where(p => p.COUNTRYID == CID && p.StateID == SID && p.CityEnglish.ToUpper().Contains(CNAME.ToUpper())).Select(p => p.CityID).FirstOrDefault();
                    obj.CITY = CityID.ToString();
                }
                else
                {
                    obj.CITY = "73";
                }

                obj.POSTALCODE = "00000";
                obj.ZIPCODE = "00000";
                obj.MYCONLOCID = 0;
                obj.MYPRODID = 1;

                if (dtExcel.Rows[i]["BUSPHONE1"].ToString().Trim() != null && dtExcel.Rows[i]["BUSPHONE1"].ToString().Trim() != "")
                    obj.BUSPHONE1 = dtExcel.Rows[i]["BUSPHONE1"].ToString();
                else
                    obj.BUSPHONE1 = "12345";

                obj.BUSPHONE1 = "12345";
                obj.BUSPHONE2 = "12345";
                obj.BUSPHONE3 = "12345";
                obj.BUSPHONE4 = "12345";

                if (dtExcel.Rows[i]["MOBPHONE"].ToString().Trim() != null && dtExcel.Rows[i]["MOBPHONE"].ToString().Trim() != "")
                    obj.MOBPHONE = dtExcel.Rows[i]["MOBPHONE"].ToString();
                else
                    obj.MOBPHONE = "12345";

                if (dtExcel.Rows[i]["Fax"].ToString().Trim() != null && dtExcel.Rows[i]["Fax"].ToString().Trim() != "")
                    obj.FAX = dtExcel.Rows[i]["Fax"].ToString();
                else
                    obj.FAX = "12345";

                if (dtExcel.Rows[i]["Fax"].ToString().Trim() != null && dtExcel.Rows[i]["Fax"].ToString().Trim() != "")
                    obj.FAX1 = dtExcel.Rows[i]["Fax"].ToString();
                else
                    obj.FAX1 = "12345";

                if (dtExcel.Rows[i]["Fax"].ToString().Trim() != null && dtExcel.Rows[i]["Fax"].ToString().Trim() != "")
                    obj.FAX2 = dtExcel.Rows[i]["Fax"].ToString();
                else
                    obj.FAX2 = "12345";

                obj.PRIMLANGUGE = "0";

                if (dtExcel.Rows[i]["WebPage"].ToString().Trim() != null && dtExcel.Rows[i]["WebPage"].ToString().Trim() != "")
                    obj.WEBPAGE = dtExcel.Rows[i]["WebPage"].ToString();
                else
                    obj.WEBPAGE = "12345";

                obj.ISMINISTRY = false;
                obj.ISSMB = false;
                obj.ISCORPORATE = false;
                obj.INHAWALLY = false;
                obj.SALER = false;
                obj.BUYER = false;
                obj.SALEDEPROD = false;
                obj.EMAISUB = false;
                obj.EMAILSUBDATE = null;
                if (dtExcel.Rows[i]["Remarks"].ToString().Trim() != null && dtExcel.Rows[i]["Remarks"].ToString().Trim() != "")
                    obj.REMARKS = dtExcel.Rows[i]["Remarks"].ToString();
                else
                    obj.REMARKS = "12345";

                if (dtExcel.Rows[i]["KeyWord"].ToString().Trim() != null && dtExcel.Rows[i]["KeyWord"].ToString().Trim() != "")
                    obj.Keyword = dtExcel.Rows[i]["KeyWord"].ToString();
                else
                    obj.Keyword = "12345";

                obj.Active = "Y";

                DB.TBLCOMPANYSETUP_ExcelImport.AddObject(obj);
                DB.SaveChanges();
            }
            GetList();
        }
        public void GetList()
        {
            Listview3.DataSource = DB.TBLCOMPANYSETUP_ExcelImport.Where(p => p.TenentID == TID);
            Listview3.DataBind();
        }
        public void Setpanel(string link, string panal)
        {
            //importsub.Style.Add("display", link);
            //importMain.Attributes["class"] = panal;
            importMain.Attributes["class"] = panal;
            I123.Style.Add("display", link);
        }

        public string GetCountryss(int Coun)
        {
            if (DB.tblCOUNTRies.Where(p => p.TenentID == TID && p.COUNTRYID == Coun).Count() > 0)
            {
                string CNAME = DB.tblCOUNTRies.Single(p => p.TenentID == TID && p.COUNTRYID == Coun).COUNAME1;
                return CNAME;
            }
            else
            {
                return "Not Found";
            }
        }

        public string GetPossition(int ID)
        {
            if (ID != 0)
            {
                List<Database.Tbl_Position_Mst> List = DB.Tbl_Position_Mst.Where(p => p.TenentID == TID && p.PositionID == ID).ToList();
                return List[0].PositionName;
            }
            else
            {
                return "Not Found";
            }

        }

        protected void btnSaveposition_Click(object sender, EventArgs e)
        {
            if (txtPOSENG.Text != null && txtPOSARB.Text != null && txtPOSFRE.Text != null)
            {
                if (DB.Tbl_Position_Mst.Where(p => p.TenentID == TID && p.PositionName.ToUpper() == txtPOSENG.Text.ToUpper()).Count() > 0)
                {
                    string msg = "This Position Is Allready Exist...";
                    string Function = "openModal('" + msg + "');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", Function, true);
                }
                else
                {
                    Database.Tbl_Position_Mst OBJ = new Database.Tbl_Position_Mst();
                    OBJ.TenentID = TID;
                    OBJ.PositionID = DB.Tbl_Position_Mst.Where(p => p.TenentID == TID).Count() > 0 ? Convert.ToInt32(DB.Tbl_Position_Mst.Where(p => p.TenentID == TID).Max(p => p.PositionID) + 1) : 1;
                    OBJ.PositionName = txtPOSENG.Text;
                    OBJ.PositionNameAR = txtPOSARB.Text;
                    OBJ.PositionNameFR = txtPOSFRE.Text;
                    OBJ.Active = true;
                    DB.Tbl_Position_Mst.AddObject(OBJ);
                    DB.SaveChanges();
                    Classes.EcommAdminClass.getdropdown(drpItManager, TID, "", "", "", "Tbl_Position_Mst");
                }
            }
        }
        public string GetEmployee(int Eid, int Cid)
        {
            List<Database.tblCONTACTBu> EMPList = DB.tblCONTACTBus.Where(p => p.TenentID == TID).ToList();
            string Name = "";
            if (EMPList.Where(p => p.ContactMyID == Eid && p.CompID == Cid).Count() == 1)
            {
                int id = EMPList.Single(p => p.ContactMyID == Eid && p.CompID == Cid).ContactMyID;
                if (DB.TBLCONTACTs.Where(p => p.TenentID == TID && p.ContactMyID == id).Count() > 0)
                {
                    Name = DB.TBLCONTACTs.Single(p => p.TenentID == TID && p.ContactMyID == id).PersName1;

                }
                return Name;
            }
            else
            {
                return "Not Found";
            }
        }
        public string GetListState(int ConID, int StaID)
        {
            if (ConID != 0 && StaID != 0)
            {
                if (DB.tblStates.Where(p => p.COUNTRYID == ConID && p.StateID == StaID).Count() > 0)
                {
                    return DB.tblStates.SingleOrDefault(p => p.COUNTRYID == ConID && p.StateID == StaID).MYNAME1;
                }
                else
                {
                    return "Not Found";
                }
            }
            else
            {
                return "Not Found";
            }
        }
        private void mouse_click(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                if (CheckBox2.Checked == true)
                {

                }
            }
        }
        protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {

            //List<Database.TBLCOMPANYSETUP_ExcelImport> Tools = new List<Database.TBLCOMPANYSETUP_ExcelImport>();
            //if (ViewState["Tools"] != null)
            //    Tools = ((List<Database.TBLCOMPANYSETUP_ExcelImport>)ViewState["Tools"]).ToList();
            //int VID = Convert.ToInt32(e.CommandArgument);

            //if (Tools.Count() == 0)
            //{
            //    Database.TBLCOMPANYSETUP_ExcelImport obj1 = new Database.TBLCOMPANYSETUP_ExcelImport();
            //    obj1.TenentID = TID;
            //    obj1.COMPID = VID;
            //    Tools.Add(obj1);
            //    ViewState["Tools"] = Tools;
            //    pnlToolss.Visible = true;
            //}
            //else
            //{
            //    if (Tools.Where(p => p.COMPID == VID).Count() >= 1)
            //    {

            //    }
            //    else
            //    {
            //        Database.TBLCOMPANYSETUP_ExcelImport obj1 = new Database.TBLCOMPANYSETUP_ExcelImport();
            //        obj1.TenentID = TID;
            //        obj1.COMPID = VID;
            //        Tools.Add(obj1);
            //        ViewState["Tools"] = Tools;
            //        pnlToolss.Visible = true;
            //    }
            //}
        }

        protected void TemplateEmail_Click(object sender, EventArgs e)
        {
            drptemplete.Visible = true;
        }

        protected void drptemplete_SelectedIndexChanged(object sender, EventArgs e)
        {
            int templateID = Convert.ToInt32(drptemplete.SelectedValue);
            string EJ = DB.Mail_Templet.Single(p => p.ID == templateID).TempletHTML_E;
            if (templateID == 0)
            {

            }
            else
            {
                ViewState["TempEmail"] = "Mail";
                string imgpath = "/CRM/images/Mail/" + DB.Mail_Templet.Single(p => p.ID == templateID).Avtar;

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "Temp('" + imgpath + "');", true);
                string msg = "HI";
                string Function = "TempHTML('" + msg + "');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", Function, true);
            }
        }

        protected void btnSendMail_Click(object sender, EventArgs e)
        {
            int MMID = 0;
            Thread SMail = new Thread(delegate()
                {
                    for (int i = 0; i < Listview1.Items.Count; i++)
                    {
                        CheckBox cbkcmpnylist = (CheckBox)Listview1.Items[i].FindControl("CheckBox1");
                        if (cbkcmpnylist.Checked == true)
                        {
                            Label lblEMAIL = (Label)Listview1.Items[i].FindControl("lblEMAIL");
                            Label hidecompanyctid = (Label)Listview1.Items[i].FindControl("Label91");
                            Label lblCustomerName = (Label)Listview1.Items[i].FindControl("lblCustomerName");
                            string Email = lblEMAIL.Text;
                            string companyID = hidecompanyctid.Text.ToString().Trim();
                            if (Email != "")
                            {
                                int Getvalue = Convert.ToInt32(drptemplete.SelectedValue);
                                string CNAME = lblCustomerName.Text.ToString();
                                string markup = DB.Mail_Templet.Single(p => p.ID == Getvalue).TempletHTML_E;
                                string markupfinal = markup.Replace("CHANGEFROMPROJECT", CNAME).Replace("CURRENTMAIL", Email).Replace("CURRENTDATE", DateTime.Now.ToShortDateString());


                                ComposeMail Obj = new ComposeMail();
                                Obj.TenentID = TID;
                                Obj.LocationID = LID;
                                Obj.MyID = DB.ComposeMails.Where(p => p.TenentID == TID).Count() > 0 ? Convert.ToInt32(DB.ComposeMails.Where(p => p.TenentID == TID).Max(p => p.MyID) + 1) : 1;
                                Obj.CompanyAndContactID = Convert.ToInt32(hidecompanyctid.Text);
                                Obj.Reference = "Company";
                                Obj.HtmlTemplate = "";
                                Obj.HtmlLink = "";
                                Obj.IsSend = sendEmail(markupfinal, Email);
                                Obj.UserId = UID;
                                Obj.DateTime = DateTime.Now;
                                Obj.TemplateId = 0;
                                DB.ComposeMails.AddObject(Obj);
                                DB.SaveChanges();

                                int EmailIDAction = 0;
                                    if (DB.tbl_Lead_Mst.Where(p => p.TenentID == TID && p.Customer_Name == "johar" && p.LeadName1 == "Dummy").Count() > 0)
                                    {
                                        Database.tbl_Lead_Mst objLead = DB.tbl_Lead_Mst.Single(p => p.TenentID == TID && p.Customer_Name == "johar" && p.LeadName1 == "Dummy");
                                        MMID = objLead.ID;
                                        if (objLead.EmailIDAction != null)
                                        {
                                            EmailIDAction = Convert.ToInt32(objLead.EmailIDAction);
                                        }
                                        int MType = Convert.ToInt32(ActionMaster.Type.Lead);
                                        int AType = Convert.ToInt32(ActionMaster.ActionType.Email);
                                        ActionGlobal.DatainsertActionMaster(MType, AType, CNAME, markupfinal, 0, TID, UID, MMID, LID, Email, "digital53marketing@gmail.com", companyID, 0, 0, 0, EmailIDAction, 0);
                                    }
                            }

                        }
                    }
                });
            SMail.IsBackground = true;
            SMail.Start();
            BindListEmail(0);
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Email sent.');", true);

            //Multiple mail at a time 19 mail send code hear

            //int Conn = 0;
            //string Email = "";
            //string markupfinal = "";
            //for (int i = 0; i < Listview1.Items.Count; i++)
            //{
            //    CheckBox cbkcmpnylist = (CheckBox)Listview1.Items[i].FindControl("CheckBox1");
            //    if (cbkcmpnylist.Checked == true)
            //    {
            //        Label lblEMAIL = (Label)Listview1.Items[i].FindControl("lblEMAIL");
            //        Conn++;
            //        if (Conn == 19)
            //            Email += lblEMAIL.Text;
            //        else
            //            Email += lblEMAIL.Text + ",";

            //        int Getvalue = Convert.ToInt32(drptemplete.SelectedValue);
            //        string CNAME = lblCustomerName.Text.ToString();
            //        string markup = DB.Mail_Templet.Single(p => p.ID == Getvalue).TempletHTML_E;
            //        markupfinal = markup.Replace("CHANGEFROMPROJECT", CNAME).Replace("CURRENTMAIL", Email).Replace("CURRENTDATE", DateTime.Now.ToShortDateString());


            //        if (Conn == 19)
            //        {
            //            sendEmail(markupfinal, Email);
            //            Conn = 0;
            //        }
            //    }

            //}
            //if (Conn < 19)
            //{
            //    sendEmail(markupfinal, Email);
            //}
        }


        public bool sendEmail(string body, string email)
        {
            try
            {
                System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();
                msg.Subject = "";
                msg.From = new System.Net.Mail.MailAddress("digital53marketing@gmail.com");//("supportteam@digital53.com ");
                msg.To.Add(new System.Net.Mail.MailAddress(email));
                msg.BodyEncoding = System.Text.Encoding.UTF8;
                msg.Body = body;
                msg.IsBodyHtml = true;
                msg.Priority = System.Net.Mail.MailPriority.High;
                System.Net.Mail.SmtpClient smpt = new System.Net.Mail.SmtpClient();
                smpt.UseDefaultCredentials = false;
                smpt.Host = "smtp.gmail.com";//for google required smtp.gmail.com and must be check Google Account setting https://myaccount.google.com/lesssecureapps?pli=1 ON//"mail.digital53.com";
                smpt.Port = 587;
                smpt.EnableSsl = true;
                //smpt.Credentials = new System.Net.NetworkCredential("supportteam@digital53.com ", "Support123$");
                smpt.Credentials = new System.Net.NetworkCredential("digital53marketing@gmail.com", "Digital123$");
                smpt.Send(msg);
                return true;

                //string[] ccid = email.ToString().Split(',');
                //foreach (string CCEmail in ccid)
                //{
                //    if (!String.IsNullOrEmpty(CCEmail))
                //    {
                //        msg.CC.Add(new System.Net.Mail.MailAddress(CCEmail));
                //    }
                //}

                //string[] Tomailid = email.ToString().Split(',');
                //foreach (string Mailid in Tomailid)
                //{
                //    if (!String.IsNullOrEmpty(Mailid))
                //    {
                //        msg.To.Add(new System.Net.Mail.MailAddress(Mailid));
                //    }
                //}
            }
            catch
            {
                return false;
            }
        }

        protected void btnEMPSAVE_Click(object sender, EventArgs e)
        {
            DateTime BDT = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            string Pername = TextBox1.Text;
            //string BirthDate = BDT.ToString("yyyy-MM-dd");
            string email = TextBox2.Text;
            string MOBPHONE = TextBox3.Text;
            string CITY = drpcity.SelectedValue.ToString();
            string STATE = drpSates.SelectedValue.ToString();
            int COUNTRYID = Convert.ToInt32(drpCountry.SelectedValue);
            int EMPCOMPID = Convert.ToInt32(txtCustoID.Text);
            int EMPID = DB.TBLCONTACTs.Where(p => p.TenentID == TID).Count() > 0 ? Convert.ToInt32(DB.TBLCONTACTs.Where(p => p.TenentID == TID).Max(p => p.ContactMyID) + 1) : 1;
            string Str = "";
            Str += "INSERT INTO [dbo].[TBLCONTACT]([TenentID],[ContactMyID],[CONTACTID],[PHYSICALLOCID],[PersName1],[PersName2],[PersName3],[BirthDate],[CivilID],[EMAIL1],[MOBPHONE],[ITMANAGER],[ADDR1],[ADDR2],[CITY],[STATE],[POSTALCODE],[ZIPCODE],[MYCONLOCID],[COUNTRYID],[BUSPHONE1],[Active],[REMARKS],[CRUP_ID],[COMPANYID],[MYCATSUBID],[MYPRODID],[DESERIAL],[CATID],[CATTYPE],[SUBCATTYPE],[SUBCATID],[PERSNAME],[PERSNAMEO],[PERSNAMEO2],[EMAIL2],[ContacType]) VALUES (" + TID + "," + EMPID + "," + EMPID + ",'WKT','" + Pername + "','" + Pername + "','" + Pername + "','" + BDT + "','0','" + email + "','" + MOBPHONE + "','0','','','" + CITY + "','" + STATE + "','0','0',0," + COUNTRYID + ",'" + MOBPHONE + "','Y','',0," + EMPCOMPID + ",0,'0','0',0,'0','0',0,'" + Pername + "','" + Pername + "','" + Pername + "','" + email + "',82016);";
            command2 = new SqlCommand(Str, con);
            con.Open();
            command2.ExecuteReader();
            con.Close();

            //Database.TBLCONTACT OBJ = new Database.TBLCONTACT();
            //int EMPID = DB.TBLCONTACTs.Where(p => p.TenentID == TID).Count() > 0 ? Convert.ToInt32(DB.TBLCONTACTs.Where(p => p.TenentID == TID).Max(p => p.ContactMyID) + 1) : 1;
            //OBJ.TenentID = TID;
            //OBJ.ContactMyID = EMPID;
            //OBJ.CONTACTID = EMPID;
            //OBJ.PHYSICALLOCID = "WKT";
            //OBJ.PersName1 = TextBox1.Text;
            //OBJ.PersName2 = TextBox1.Text;
            //OBJ.PersName3 = TextBox1.Text;            
            //OBJ.BirthDate = BDT;
            //OBJ.CivilID = "0";
            //OBJ.EMAIL1 = TextBox2.Text;
            //OBJ.MOBPHONE = TextBox3.Text;
            //OBJ.ITMANAGER = "0";
            //OBJ.ADDR1 = "";
            //OBJ.ADDR2 = "";
            //OBJ.CITY = drpcity.SelectedValue.ToString();
            //OBJ.STATE = drpSates.SelectedValue.ToString();
            //OBJ.POSTALCODE = "0";
            //OBJ.ZIPCODE = "0";
            //OBJ.MYCONLOCID = 0;
            //OBJ.COUNTRYID = Convert.ToInt32(drpCountry.SelectedValue);
            //OBJ.BUSPHONE1 = TextBox3.Text;
            //OBJ.Active = "Y";
            //OBJ.REMARKS = "";
            //OBJ.CRUP_ID = 0;
            //OBJ.COMPANYID = EMPCOMPID;
            //OBJ.MYCATSUBID = 0;
            //OBJ.MYPRODID = "0";
            //OBJ.DESERIAL = "0";
            //OBJ.CATID = 0;
            //OBJ.CATTYPE = "0";
            //OBJ.SUBCATTYPE = "0";
            //OBJ.SUBCATID = 0;
            //OBJ.PERSNAME = TextBox1.Text;
            //OBJ.PERSNAMEO = TextBox1.Text;
            //OBJ.PERSNAMEO2 = TextBox1.Text;
            //OBJ.EMAIL2 = TextBox2.Text;
            //OBJ.ContacType = 82016;
            //DB.TBLCONTACTs.AddObject(OBJ);
            //DB.SaveChanges();

            string SQOCommad = "Select * from tblCONTACTBus where TenentID=" + TID + " and ContactMyID=" + EMPID + " and CompID=" + EMPCOMPID + ";";
            SqlCommand CMD1 = new SqlCommand(SQOCommad, con);
            SqlDataAdapter ADB = new SqlDataAdapter(CMD1);
            DataSet ds = new DataSet();
            ADB.Fill(ds);
            DataTable dt = ds.Tables[0];

            if (dt.Rows.Count == 0)
            {


                //if (DB.tblCONTACTBus.Where(p => p.TenentID == TID && p.ContactMyID == EMPID && p.CompID == EMPCOMPID).Count() == 0)
                //{
                string JobTitle = DrpPopPosition.SelectedValue.ToString();
                string str1 = "";
                str1 += "INSERT INTO [dbo].[tblCONTACTBus]([TenentID],[BusPhone1],[ContactMyID],[CompID],[Country],[email2],[Fax],[JobTitle],[PhysicalLocID],[Active],[remarks])VALUES(" + TID + ",'" + MOBPHONE + "'," + EMPID + "," + EMPCOMPID + "," + COUNTRYID + ",'" + email + "','','" + JobTitle + "','WKT','Y','');";
                command2 = new SqlCommand(str1, con);
                con.Open();
                command2.ExecuteReader();
                con.Close();

                //Database.tblCONTACTBu objtbl_CONTACTBus = new Database.tblCONTACTBu();
                //objtbl_CONTACTBus.TenentID = TID;
                //objtbl_CONTACTBus.BusPhone1 = TextBox3.Text;
                //objtbl_CONTACTBus.ContactMyID = EMPID;
                //objtbl_CONTACTBus.CompID = EMPCOMPID;
                //objtbl_CONTACTBus.Country = drpCountry.SelectedValue.ToString();
                //objtbl_CONTACTBus.email2 = TextBox2.Text;
                //objtbl_CONTACTBus.Fax = "";
                //objtbl_CONTACTBus.JobTitle = DrpPopPosition.SelectedValue.ToString();
                //objtbl_CONTACTBus.PhysicalLocID = "WKT";
                //objtbl_CONTACTBus.remarks = "";
                //objtbl_CONTACTBus.Active = "Y";
                //DB.tblCONTACTBus.AddObject(objtbl_CONTACTBus);
                //DB.SaveChanges();

                BindCompanywisecontact(EMPCOMPID);
                //}

            }
            TextBox1.Text = "";
            if (!String.IsNullOrEmpty(tags_2.Text))
            {
                string[] TMAIL = tags_2.Text.Split(',');
                string Submail = TMAIL[0];
                TextBox2.Text = Submail;
            }
            //TextBox2.Text = tags_2.Text;
            if (!String.IsNullOrEmpty(txtMobileNo.Text))
            {
                string[] TMobNO = txtMobileNo.Text.Split(',');
                string SubTMobNO = TMobNO[0];
                TextBox3.Text = SubTMobNO;
            }
            //TextBox3.Text = txtMobileNo.Text;

            //bgw.DoWork += new DoWorkEventHandler(bgw_DoWork);            
            //bgw.ProgressChanged += new ProgressChangedEventHandler(bgw_ProgressChanged);
            //bgw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bgw_RunWorkerCompleted);
            //bgw.WorkerReportsProgress = true;
            //bgw.RunWorkerAsync();

            string msg = "Employee save sucessfully...";
            string Function = "openModalsmall2('" + msg + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", Function, true);

        }
        //BackgroundWorker bgw = new BackgroundWorker();
        //void bgw_DoWork(object sender, DoWorkEventArgs e)
        //{
        //    int total = 57; //some number (this is your variable to change)!!

        //    for (int i = 0; i <= total; i++) //some number (total)
        //    {
        //        System.Threading.Thread.Sleep(100);
        //        int percents = (i * 100) / total;
        //        bgw.ReportProgress(percents, i);
        //        //2 arguments:
        //        //1. procenteges (from 0 t0 100) - i do a calcumation 
        //        //2. some current value!
        //    }
        //}
        //protected void ProgressBar1_RunTask1(object sender, EO.Web.ProgressTaskEventArgs e)
        //{
        //    for (int i = 0; i < 100; i++)
        //    {
        //        //Check whether the task has been stopped
        //        if (e.IsStopped)
        //            break;

        //        //Perform some work
        //        System.Threading.Thread.Sleep(1000);

        //        //Update client side progress
        //        e.UpdateProgress(i,null);
        //    }
        //}
        //void bgw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        //{
        //    ProgressBar1.Value = e.ProgressPercentage;
        //    //label1.Text = String.Format("Progress: {0} %", e.ProgressPercentage);
        //    //label2.Text = String.Format("Total items transfered: {0}", e.UserState);
        //}

        //void bgw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        //{
        //    //do the code when bgv completes its work
        //}


        //private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        //{
        //    // TODO: do something with final calculation.
        //}
        protected void lstCompniy_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (e.CommandName == "LinkEMPEDIT")
            {
                string[] EID = e.CommandArgument.ToString().Split('-');
                int EIDContact = Convert.ToInt32(EID[0]);
                int EIDCompany = Convert.ToInt32(EID[1]);

                Label lblEMP = (Label)e.Item.FindControl("lblEMP");
                TextBox txtEMP = (TextBox)e.Item.FindControl("txtEMP");
                Label lblEMAIL = (Label)e.Item.FindControl("lblEMAIL");
                TextBox txtEMAIL = (TextBox)e.Item.FindControl("txtEMAIL");
                Label lblMOBPHONE = (Label)e.Item.FindControl("lblMOBPHONE");
                TextBox txtMOBPHONE = (TextBox)e.Item.FindControl("txtMOBPHONE");
                LinkButton LinkEMPEDIT = (LinkButton)e.Item.FindControl("LinkEMPEDIT");
                LinkButton LinkEditSave = (LinkButton)e.Item.FindControl("LinkEditSave");
                Label lblAddress = (Label)e.Item.FindControl("lblAddress");
                DropDownList DrpEDTPosition = (DropDownList)e.Item.FindControl("DrpEDTPosition");
                Label Label117 = (Label)e.Item.FindControl("Label117");


                lblEMP.Visible = false;
                txtEMP.Visible = true;
                lblEMAIL.Visible = false;
                txtEMAIL.Visible = true;
                lblMOBPHONE.Visible = false;
                txtMOBPHONE.Visible = true;
                LinkEMPEDIT.Visible = false;
                LinkEditSave.Visible = true;
                lblAddress.Visible = false;
                DrpEDTPosition.Visible = true;
                txtEMP.Text = lblEMP.Text;
                txtEMAIL.Text = lblEMAIL.Text;
                txtMOBPHONE.Text = lblMOBPHONE.Text;
                DrpEDTPosition.SelectedValue = Label117.Text.ToString();
            }
            if (e.CommandName == "LinkEditSave")
            {

                string[] EID = e.CommandArgument.ToString().Split('-');
                int EIDContact = Convert.ToInt32(EID[0]);
                int EIDCompany = Convert.ToInt32(EID[1]);

                Label lblEMP = (Label)e.Item.FindControl("lblEMP");
                TextBox txtEMP = (TextBox)e.Item.FindControl("txtEMP");
                Label lblEMAIL = (Label)e.Item.FindControl("lblEMAIL");
                TextBox txtEMAIL = (TextBox)e.Item.FindControl("txtEMAIL");
                Label lblMOBPHONE = (Label)e.Item.FindControl("lblMOBPHONE");
                TextBox txtMOBPHONE = (TextBox)e.Item.FindControl("txtMOBPHONE");
                LinkButton LinkEMPEDIT = (LinkButton)e.Item.FindControl("LinkEMPEDIT");
                LinkButton LinkEditSave = (LinkButton)e.Item.FindControl("LinkEditSave");
                Label lblAddress = (Label)e.Item.FindControl("lblAddress");
                DropDownList DrpEDTPosition = (DropDownList)e.Item.FindControl("DrpEDTPosition");

                string Str = "";
                Str += "update TBLCONTACT set PersName1='" + txtEMP.Text + "',PersName2='" + txtEMP.Text + "',PersName3='" + txtEMP.Text + "',EMAIL1='" + txtEMAIL.Text + "',MOBPHONE='" + txtMOBPHONE.Text + "' where TenentID=" + TID + " and ContactMyID=" + EIDContact + ";";
                Str += "update tblCONTACTBus set BusPhone1='" + txtMOBPHONE.Text + "',email2='" + txtEMAIL.Text + "',JobTitle='" + DrpEDTPosition.SelectedValue + "' where TenentID=" + TID + " and ContactMyID=" + EIDContact + " and CompID=" + EIDCompany + ";";
                command2 = new SqlCommand(Str, con);
                con.Open();
                command2.ExecuteReader();
                con.Close();
                //Database.TBLCONTACT EOBJ = DB.TBLCONTACTs.Single(p => p.TenentID == TID && p.ContactMyID == EIDContact);
                //EOBJ.PersName1 = txtEMP.Text;
                //EOBJ.PersName2 = txtEMP.Text;
                //EOBJ.PersName3 = txtEMP.Text;

                //DB.TBLCONTACTs.AddObject(EOBJ);
                //DB.SaveChanges();

                lblEMP.Visible = true;
                txtEMP.Visible = false;
                LinkEMPEDIT.Visible = true;
                LinkEditSave.Visible = false;
                lblEMAIL.Visible = true;
                txtEMAIL.Visible = false;
                lblMOBPHONE.Visible = true;
                txtMOBPHONE.Visible = false;
                lblAddress.Visible = true;
                DrpEDTPosition.Visible = false;
                txtEMP.Text = "";
                txtEMAIL.Text = "";
                txtMOBPHONE.Text = "";
                DrpEDTPosition.SelectedValue = "0";
                BindCompanywisecontact(EIDCompany);
            }
            if (e.CommandName == "LinkEMPDELETE")
            {
                string[] EID = e.CommandArgument.ToString().Split('-');
                int EIDContact = Convert.ToInt32(EID[0]);
                int EIDCompany = Convert.ToInt32(EID[1]);

                Database.tblCONTACTBu EditObj = DB.tblCONTACTBus.Single(p => p.TenentID == TID && p.ContactMyID == EIDContact && p.CompID == EIDCompany);
                DB.tblCONTACTBus.DeleteObject(EditObj);
                DB.SaveChanges();

                BindCompanywisecontact(EIDCompany);

                string msg = "Employee save sucessfull...";
                string Function = "openModal('" + msg + "');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", Function, true);
            }
        }
        public System.Drawing.Image MakeBarcode(string InputData, int BarWeight, bool AddQuietZone)
        {
            // get the Code128 codes to represent the message
            Code128Content content = new Code128Content(InputData);
            int[] codes = content.Codes;

            int width, height;
            width = ((codes.Length - 3) * 11 + 35) * BarWeight;
            height = Convert.ToInt32(System.Math.Ceiling(Convert.ToSingle(width) * .15F));

            if (AddQuietZone)
            {
                width += 2 * cQuietWidth * BarWeight;  // on both sides
            }

            // get surface to draw on
            System.Drawing.Image myimg = new System.Drawing.Bitmap(width, height);
            using (Graphics gr = Graphics.FromImage(myimg))
            {

                // set to white so we don't have to fill the spaces with white
                gr.FillRectangle(System.Drawing.Brushes.White, 0, 0, width, height);

                // skip quiet zone
                int cursor = AddQuietZone ? cQuietWidth * BarWeight : 0;

                for (int codeidx = 0; codeidx < codes.Length; codeidx++)
                {
                    int code = codes[codeidx];

                    // take the bars two at a time: a black and a white
                    for (int bar = 0; bar < 8; bar += 2)
                    {
                        int barwidth = cPatterns[code, bar] * BarWeight;
                        int spcwidth = cPatterns[code, bar + 1] * BarWeight;

                        // if width is zero, don't try to draw it
                        if (barwidth > 0)
                        {
                            gr.FillRectangle(System.Drawing.Brushes.Black, cursor, 0, barwidth, height);
                        }

                        // note that we never need to draw the space, since we 
                        // initialized the graphics to all white

                        // advance cursor beyond this pair
                        cursor += (barwidth + spcwidth);
                    }
                }
            }

            return myimg;

        }
        public void showBarcode(string Bar)
        {
            //Image1.ImageUrl = "~/Master/Img/" + Bar + ".png";

            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeGenerator.QRCode qrCode = qrGenerator.CreateQrCode(Bar, QRCodeGenerator.ECCLevel.Q);
            System.Web.UI.WebControls.Image imgBarCode1 = new System.Web.UI.WebControls.Image();
            imgBarCode1.Height = 150;
            imgBarCode1.Width = 150;
            using (Bitmap bitMap = qrCode.GetGraphic(20))
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    bitMap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                    byte[] byteImage = ms.ToArray();
                    imgBarCode1.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(byteImage);
                }
                plBarCode.Controls.Add(imgBarCode1);
            }
        }

        protected void lstCompniy_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            DropDownList DrpEDTPosition = (DropDownList)e.Item.FindControl("DrpEDTPosition");
            Classes.EcommAdminClass.getdropdown(DrpEDTPosition, TID, "", "", "", "Tbl_Position_Mst");
        }

        protected void btnSaveandConti_Click(object sender, EventArgs e)
        {
            PanelError.Visible = false;
            lblerror.Text = "";
            if (txtCustoID.Text != "")
            {
                //int CID = Convert.ToInt32(txtCustoID.Text);
                //if (DB.TBLCOMPANYSETUPs.Where(p => p.TenentID == TID && p.COMPID == CID).Count() > 0)
                //{
                //    PanelError.Visible = true;
                //    lblerror.Text = "Customer ID Already Exist";
                //    return;
                //}
            }
            else
            {
                PanelError.Visible = true;
                lblerror.Text = "Please Enter Customer ID";
                return;
            }




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

                txtCustoID.Text = COMPID.ToString();

                objtbl_COMPANYSETUP.TenentID = TID;
                objtbl_COMPANYSETUP.PACI = txtPACI.Text;
                objtbl_COMPANYSETUP.PHYSICALLOCID = drplocation.SelectedValue;
                objtbl_COMPANYSETUP.COMPNAME1 = txtCustomerName.Text;
                objtbl_COMPANYSETUP.COMPNAME2 = txtCustomer.Text;
                objtbl_COMPANYSETUP.COMPNAME3 = txtCustomer2.Text;
                objtbl_COMPANYSETUP.EMAIL1 = getFirstCommaseprate(tags_2.Text);
                // objtbl_COMPANYSETUP.EMAIL2 = tags_2.Text;
                objtbl_COMPANYSETUP.ITMANAGER = drpItManager.SelectedValue;
                objtbl_COMPANYSETUP.ADDR1 = txtAddress.Text;
                objtbl_COMPANYSETUP.ADDR2 = txtAddress2.Text;
                if (txtBirthdate.Text != "")
                {
                    objtbl_COMPANYSETUP.BirthDate = Convert.ToDateTime(txtBirthdate.Text);
                }

                objtbl_COMPANYSETUP.CivilID = txtCivilID.Text;
                //objtbl_COMPANYSETUP.CITY = txtCity.Text;
                objtbl_COMPANYSETUP.CITY = drpcity.SelectedValue;
                objtbl_COMPANYSETUP.STATE = drpSates.SelectedValue;
                objtbl_COMPANYSETUP.POSTALCODE = txtPostalCode.Text;
                objtbl_COMPANYSETUP.ZIPCODE = txtZipCode.Text;
                //objtbl_COMPANYSETUP.MYCONLOCID = Convert.ToInt32(drpMyCounLocID.SelectedValue);
                objtbl_COMPANYSETUP.MYPRODID = Convert.ToInt32(drpMyProductId.SelectedValue);
                //   string CNAME = drpCountry.SelectedItem.ToString();
                objtbl_COMPANYSETUP.COUNTRYID = Convert.ToInt32(drpCountry.SelectedValue);
                objtbl_COMPANYSETUP.BUSPHONE1 = getFirstCommaseprate(tags_4.Text);
                //objtbl_COMPANYSETUP.BUSPHONE2 = tags_4.Text;
                //objtbl_COMPANYSETUP.BUSPHONE3 = tags_4.Text;
                //objtbl_COMPANYSETUP.BUSPHONE4 = tags_4.Text;
                objtbl_COMPANYSETUP.MOBPHONE = getFirstCommaseprate(txtMobileNo.Text);
                objtbl_COMPANYSETUP.BARCODE = txtBarCode.Text;
                objtbl_COMPANYSETUP.FAX = getFirstCommaseprate(tags_3.Text);
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
                objtbl_COMPANYSETUP.Marketting = txtrefreshno.Text;
                objtbl_COMPANYSETUP.CUSERID = txtcUserName.Text;
                objtbl_COMPANYSETUP.CPASSWRD = txtcPassword.Text;
                objtbl_COMPANYSETUP.USERID = UID;
                if (drpdatasource.SelectedValue != "0")
                    objtbl_COMPANYSETUP.datasource = Convert.ToInt32(drpdatasource.SelectedValue);
                //objtbl_COMPANYSETUP.ENTRYDATE = DateTime.Now;
                //objtbl_COMPANYSETUP.ENTRYTIME = DateTime.Now;
                objtbl_COMPANYSETUP.UPDTTIME = DateTime.Now;
                objtbl_COMPANYSETUP.Approved = 0;

                if (avatarUploadd.HasFile)
                {
                    string path = COMPID + "_" + TID + "_" + txtCustomerName.Text + "_TBLCOMPANYSETUP_" + avatarUploadd.FileName;
                    avatarUploadd.SaveAs(Server.MapPath("/CRM/Upload/" + path));
                    objtbl_COMPANYSETUP.Avtar = path;
                }


                DB.SaveChanges();


                Classes.Toastr.ShowToast(Page, Classes.Toastr.ToastType.Success, "Data Edit Successfully", "Success!", Classes.Toastr.ToastPosition.TopCenter);


            }
            else
            {
                bool EDTFalge = false;
                int CID1 = Convert.ToInt32(txtCustoID.Text);
                if (DB.TBLCOMPANYSETUPs.Where(p => p.TenentID == TID && p.COMPID == CID1).Count() > 0)
                {
                    EDTFalge = true;
                }
                int COMPID = Convert.ToInt32(txtCustoID.Text);
                int SeqCount = DB.TBLCOMPANYSETUPs.Where(p => p.TenentID == TID).Count();
                Database.TBLCOMPANYSETUP objtbl_COMPANYSETUP = new Database.TBLCOMPANYSETUP();
                objtbl_COMPANYSETUP.TenentID = TID;
                objtbl_COMPANYSETUP.COMPID = COMPID;// DB.TBLCOMPANYSETUPs.Where(p => p.TenentID == TID).Count() > 0 ? Convert.ToInt32(DB.TBLCOMPANYSETUPs.Where(p => p.TenentID == TID).Max(p => p.COMPID) + 1) : 1;
                ViewState["CIDRF"] = Convert.ToInt32(objtbl_COMPANYSETUP.COMPID);
                objtbl_COMPANYSETUP.PACI = txtPACI.Text;
                objtbl_COMPANYSETUP.PHYSICALLOCID = drplocation.SelectedValue;
                objtbl_COMPANYSETUP.COMPNAME1 = txtCustomerName.Text;
                objtbl_COMPANYSETUP.COMPNAME2 = txtCustomer.Text;
                objtbl_COMPANYSETUP.COMPNAME3 = txtCustomer2.Text;
                objtbl_COMPANYSETUP.EMAIL1 = getFirstCommaseprate(tags_2.Text);
                // objtbl_COMPANYSETUP.EMAIL2 = tags_2.Text;
                objtbl_COMPANYSETUP.ITMANAGER = drpItManager.SelectedValue;
                objtbl_COMPANYSETUP.ADDR1 = txtAddress.Text;
                objtbl_COMPANYSETUP.ADDR2 = txtAddress2.Text;
                if (txtBirthdate.Text != "")
                {
                    objtbl_COMPANYSETUP.BirthDate = Convert.ToDateTime(txtBirthdate.Text);
                }

                objtbl_COMPANYSETUP.CivilID = txtCivilID.Text;
                //objtbl_COMPANYSETUP.CITY = txtCity.Text;
                objtbl_COMPANYSETUP.CITY = drpcity.SelectedValue;
                objtbl_COMPANYSETUP.STATE = drpSates.SelectedValue;
                objtbl_COMPANYSETUP.POSTALCODE = txtPostalCode.Text;
                objtbl_COMPANYSETUP.ZIPCODE = txtZipCode.Text;
                //objtbl_COMPANYSETUP.MYCONLOCID = Convert.ToInt32(drpMyCounLocID.SelectedValue);
                objtbl_COMPANYSETUP.MYPRODID = Convert.ToInt32(drpMyProductId.SelectedValue);
                //  string CNAME = drpCountry.SelectedItem.ToString();
                objtbl_COMPANYSETUP.COUNTRYID = Convert.ToInt32(drpCountry.SelectedValue);
                objtbl_COMPANYSETUP.BUSPHONE1 = getFirstCommaseprate(tags_4.Text);
                //objtbl_COMPANYSETUP.BUSPHONE2 = tags_4.Text;
                //objtbl_COMPANYSETUP.BUSPHONE3 = tags_4.Text;
                //objtbl_COMPANYSETUP.BUSPHONE4 = tags_4.Text;
                objtbl_COMPANYSETUP.MOBPHONE = getFirstCommaseprate(txtMobileNo.Text);

                objtbl_COMPANYSETUP.FAX = getFirstCommaseprate(tags_3.Text);
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
                objtbl_COMPANYSETUP.Marketting = txtrefreshno.Text;
                objtbl_COMPANYSETUP.CUSERID = txtcUserName.Text;
                objtbl_COMPANYSETUP.CPASSWRD = txtcPassword.Text;
                objtbl_COMPANYSETUP.USERID = UID;
                objtbl_COMPANYSETUP.ENTRYDATE = DateTime.Now;
                objtbl_COMPANYSETUP.ENTRYTIME = DateTime.Now;
                objtbl_COMPANYSETUP.UPDTTIME = DateTime.Now;

                if (drpdatasource.SelectedValue != "0")
                    objtbl_COMPANYSETUP.datasource = Convert.ToInt32(drpdatasource.SelectedValue);

                objtbl_COMPANYSETUP.Approved = 0;
                objtbl_COMPANYSETUP.Active = "Y";

                if (avatarUploadd.HasFile)
                {
                    string path = COMPID + "_" + TID + "_" + txtCustomerName.Text + "_TBLCOMPANYSETUP_" + avatarUploadd.FileName;
                    avatarUploadd.SaveAs(Server.MapPath("/CRM/Upload/" + path));
                    objtbl_COMPANYSETUP.Avtar = path;
                }
                if (txtBarCode.Text != "")
                {
                    //string str = (Server.MapPath("/CRM/images/" + txtBarCode.Text + ".png"));                       
                    objtbl_COMPANYSETUP.BARCODE = txtBarCode.Text;

                }



                if (EDTFalge == false)
                {
                    DB.TBLCOMPANYSETUPs.AddObject(objtbl_COMPANYSETUP);
                }
                DB.SaveChanges();
                LinkAddEmployee.Attributes["Style"] = "display:block;";

            }

            //BindData();
            // clen();
            //redonlyfalse();
            //btnSubmit.Visible = false;
            //Button1.Visible = true;
            //txtCustoID.Enabled = false;
            //btnattmentmst.Visible = false;
            //PanalFBImage.Visible = false;

        }

        protected void txtMobileNo_TextChanged2(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtMobileNo.Text))
            {
                string[] TMobNO = txtMobileNo.Text.Split(',');
                string SubTMobNO = TMobNO[0];
                TextBox3.Text = SubTMobNO;
            }
            //string mob = txtMobileNo.Text;
            //TextBox3.Text = mob;
        }

        protected void tags_2_TextChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(tags_2.Text))
            {
                string[] TMAIL = tags_2.Text.Split(',');
                string Submail = TMAIL[0];
                TextBox2.Text = Submail;
            }
            //string mail = tags_2.Text;
            //TextBox2.Text = mail;
        }

        protected void ImageReader_Click(object sender, EventArgs e)
        {
            string url = "http://imageconvert.nsspot.net/?m=img2ocr";
            string s = "window.open('" + url + "', 'popup_window', 'width=500,height=500,left=100,top=100,scrollbars=yes,resizable=yes');";
            ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
        }

        protected void drpmarketing_SelectedIndexChanged(object sender, EventArgs e)
        {
            string tagdt;
            string drpval = drpmarketing.SelectedItem.ToString();
            if (tags_1.Text != "")
            {
                tagdt = txtrefreshno.Text;
                tagdt += "," + drpval;
                txtrefreshno.Text = tagdt;
            }
            else
            {
                tagdt = drpval;
                txtrefreshno.Text = tagdt;
            }
        }

        protected void LinkTools_Click(object sender, EventArgs e)
        {
            //CheckBox checkbox1 = (CheckBox)sender;
            //ListViewDataItem item = (ListViewDataItem)checkbox1.NamingContainer;

            //CheckBox Lcheck = (CheckBox)item.FindControl("checkbox1");

            for (int i = 0; i < Listview1.Items.Count; i++)
            {
                CheckBox checkbox1 = (CheckBox)Listview1.Items[i].FindControl("checkbox1");
                if (checkbox1.Checked == true)
                {
                    pnlToolss.Visible = true;
                    break;
                }
                else
                {
                    pnlToolss.Visible = false;
                }

            }


        }

        protected void btnaddMEM_Click(object sender, EventArgs e)
        {
            int MMID = 0;
            for (int i = 0; i < Listview1.Items.Count; i++)
            {
                CheckBox cbkcmpnylist = (CheckBox)Listview1.Items[i].FindControl("CheckBox1");
                Label hidecompanyctid = (Label)Listview1.Items[i].FindControl("Label91");
                Label lblCustomerName = (Label)Listview1.Items[i].FindControl("lblCustomerName");
                string CNAME = lblCustomerName.Text;
                string TO = ((USER_MST)Session["USER"]).LOGIN_ID.ToString();
                string companyID = hidecompanyctid.Text.ToString().Trim();
                int Comp = Convert.ToInt32(companyID);
                if (cbkcmpnylist.Checked == true)
                {

                    int MType = Convert.ToInt32(ActionMaster.Type.Lead);
                    int AType = Convert.ToInt32(ActionMaster.ActionType.Memo);

                    int MemoID = 0;
                    string memoTitle = txtMOMTitle.Text;
                    string memoDesc = txtMOMDESC.Text;
                    DateTime? Ndate = null;
                    if (txtnotedate.Text != "")
                        Ndate = Convert.ToDateTime(txtnotedate.Text);
                    if (DB.tbl_Lead_Mst.Where(p => p.TenentID == TID && p.Customer_Name == "johar" && p.LeadName1 == "Dummy").Count() > 0)
                    {
                        Database.tbl_Lead_Mst objLead = DB.tbl_Lead_Mst.Single(p => p.TenentID == TID && p.Customer_Name == "johar" && p.LeadName1 == "Dummy");
                        MMID = objLead.ID;
                        if (objLead.MemoID != null)
                        {
                            MemoID = Convert.ToInt32(objLead.MemoID);
                        }
                        int NID = ActionGlobal.CRMDatainsertActionMaster(MType, AType, memoTitle, memoDesc, 0, TID, UID, MMID, LID, CNAME, TO, companyID, 0, 0, 0, 0, MemoID, Ndate);
                        //CRM_MainActivity & SubActivity
                        string ACTIVITYE = "CRM_NOTE";
                        string UNAME = ((USER_MST)Session["USER"]).LOGIN_ID.ToString();
                        string CampynDescription = "";
                        int MDUID = 0;
                        if (Session["SiteModuleID"] != null)
                        {
                            MDUID = Convert.ToInt32(Session["SiteModuleID"]);
                        }
                        int Myid = DB.CRMMainActivities.Where(p => p.TenentID == TID && p.COMPID == Comp).Count() > 0 ? Convert.ToInt32(DB.CRMMainActivities.Where(p => p.TenentID == TID && p.COMPID == Comp).Max(p => p.MyID) + 1) : 1;
                        int Activityid = DB.CRMActivities.Where(p => p.TenentID == TID && p.COMPID == Comp).Count() > 0 ? Convert.ToInt32(DB.CRMActivities.Where(p => p.TenentID == TID && p.COMPID == Comp).Max(p => p.ActivityID) + 1) : 1;
                        int LMN = Classes.CRMClass.InserActivityMain(TID, Comp, LID, Myid, 1, UID, ACTIVITYE, UNAME, MDUID, Activityid, "CRM_Note_Activity", CNAME, CampynDescription);
                        //CRM_PROGRESS_HomeWork
                        int StatusID = 10610;
                        string ButtionName = "Note Reminder";
                        if (Ndate == null)
                            ButtionName = "No Status";
                        else
                            ButtionName = "Note Reminder";
                        bool Allowed = true;
                        string Parameter2 = NID.ToString();
                        string Parameter3 = "";
                        bool Active = true;
                        DateTime Datetime = DateTime.Now;
                        string URL = "/CRM/CompanyMaster.aspx";
                        string URLWrite = "";
                        Classes.ACMClass.InsertDataCRMProgHW(TID, Activityid, StatusID, ButtionName, Allowed, Parameter2, Parameter3, Active, Datetime, 0, Ndate, URL, URLWrite, LMN);
                    }
                }
            }
            Bindmemo(0);

        }
        //public void InsertCRMMainActivity(int COMPID1, int TransNo, string Status, string ADDACTIvity, string CampName, string Desctipion)
        //{
        //    string UNAME = ((USER_MST)Session["USER"]).FIRST_NAME.ToString();
        //    int MDUID = 0;
        //    if (Session["SiteModuleID"] != null)
        //    {
        //        MDUID = Convert.ToInt32(Session["SiteModuleID"]);
        //    }


        //    int TenentID = TID;
        //    int LocationID = LID;
        //    int USERCODE = UID;
        //    int COMPID = COMPID1;

        //    int ID = TransNo;
        //    int RouteID = 1;
        //    string ACTIVITYE = "Transaction";
        //    DateTime REPEATTILL = DateTime.Now;
        //    string USERNAME = UNAME;
        //    string Version1 = UNAME;
        //    string MyStatus = Status;
        //    string RecodType = ADDACTIvity;
        //    string URL = Request.Url.AbsolutePath;
        //    string CampynName = CampName + "  " + TransNo;
        //    string CampynDescription = Desctipion;
        //    Classes.CRMClass.InserActivityMain(TenentID, COMPID, LocationID, ID, RouteID, UID, ACTIVITYE, UNAME, MDUID, 5, "Purchase Return Final", CampynName, CampynDescription);
        //}
        protected void btnAPOSave_Click(object sender, EventArgs e)
        {
            int MMID = 0;
            for (int i = 0; i < Listview1.Items.Count; i++)
            {
                CheckBox cbkcmpnylist = (CheckBox)Listview1.Items[i].FindControl("CheckBox1");
                Label hidecompanyctid = (Label)Listview1.Items[i].FindControl("Label91");
                Label lblCustomerName = (Label)Listview1.Items[i].FindControl("lblCustomerName");
                string CNAME = lblCustomerName.Text;
                string TO = ((USER_MST)Session["USER"]).LOGIN_ID.ToString();
                string companyID = hidecompanyctid.Text.ToString().Trim();
                if (cbkcmpnylist.Checked == true)
                {
                    int MType = Convert.ToInt32(ActionMaster.Type.Lead);
                    int AType = Convert.ToInt32(ActionMaster.ActionType.Appointment);

                    string Title = txtAPOtitle.Text;
                    string status = drpAPOStatus.SelectedValue;
                    DateTime sdate = Convert.ToDateTime(txtAPOSDT.Text);
                    DateTime edate = Convert.ToDateTime(txtAPOEDT.Text);

                    int AppointID = 0;
                    if (DB.tbl_Lead_Mst.Where(p => p.TenentID == TID && p.Customer_Name == "johar" && p.LeadName1 == "Dummy").Count() > 0)
                    {
                        Database.tbl_Lead_Mst objLead = DB.tbl_Lead_Mst.Single(p => p.TenentID == TID && p.Customer_Name == "johar" && p.LeadName1 == "Dummy");
                        MMID = objLead.ID;
                        if (objLead.AppointmentID != null)
                        {
                            AppointID = Convert.ToInt32(objLead.AppointmentID);
                        }
                        Classes.CRMClass.InsertAppointment(0, TID, 1, Title, sdate, edate, status, "Demo.com", true, true, companyID, "Insert", AppointID, MType, AType, CNAME, TO);
                        int NID = ActionGlobal.CRMDatainsertActionMaster(MType, AType, Title, "", 0, TID, UID, MMID, LID, CNAME, TO, companyID, 0, 0, 0, 0, AppointID, sdate);
                    }
                }
            }
            BindAppoint(0);
        }
        public void BindAppoint(int CID)
        {
            string CCID = CID.ToString();
            if (CID == 0)
            {
                ListAppint.DataSource = DB.Appointments.Where(p => p.TenentID == TID && p.Type == 2 && p.ActionType == 7);
                ListAppint.DataBind();
            }
            else
            {
                ListAppint.DataSource = DB.Appointments.Where(p => p.TenentID == TID && p.Type == 2 && p.ActionType == 7 && p.CRMReference == CCID);
                ListAppint.DataBind();
            }
        }
        public void Bindmemo(int CID)
        {
            string CCID = CID.ToString();
            if (CID == 0)
            {
                ListMemo.DataSource = DB.ISActionMasters.Where(p => p.TenantedID == TID && p.Type == 2 && p.ActionType == 3).OrderByDescending(p => p.TransactionDate);
                ListMemo.DataBind();
            }
            else
            {
                ListMemo.DataSource = DB.ISActionMasters.Where(p => p.TenantedID == TID && p.Type == 2 && p.ActionType == 3 && p.Switch3 == CCID).OrderByDescending(p => p.TransactionDate);
                ListMemo.DataBind();
            }
        }
        public void BindListEmail(int CID)
        {
            string CCID = CID.ToString();
            if (CID == 0)
            {
                ListEmail.DataSource = DB.ISActionMasters.Where(p => p.TenantedID == TID && p.Type == 2 && p.ActionType == 6);
                ListEmail.DataBind();
            }
            else
            {
                ListEmail.DataSource = DB.ISActionMasters.Where(p => p.TenantedID == TID && p.Type == 2 && p.ActionType == 6 && p.Title == CCID);
                ListEmail.DataBind();
            }
        }
        protected void ListAppint_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            Label Label124 = (Label)e.Item.FindControl("Label124");
            Label Label121 = (Label)e.Item.FindControl("Label121");


            string ID = Label124.Text.Trim().ToString();
            if (ID == "red")
            {
                Label121.Text = "Not Confirmed";
                Label121.Attributes["style"] = "background-color:red;color:#fff;text-align:center;";

            }
            else if (ID == "green")
            {
                Label121.Text = "Confirmed";
                Label121.Attributes["style"] = "background-color:green;color:#fff;text-align:center;";
            }
            else if (ID == "blue")
            {
                Label121.Text = "No Answer";
                Label121.Attributes["style"] = "background-color:blue;color:#fff;text-align:center;";
            }
            else if (ID == "yellow")
            {
                Label121.Text = "In Waiting";
                Label121.Attributes["style"] = "background-color:yellow;color:#fff;text-align:center;";
            }
            else if (ID == "purple")
            {
                Label121.Text = "Visited";
                Label121.Attributes["style"] = "background-color:purple;color:#fff;text-align:center;";
            }
            else if (ID == "gray")
            {
                Label121.Text = "Closed";
                Label121.Attributes["style"] = "background-color:gray;color:#fff;text-align:center;";
            }
            else if (ID == "indigo")
            {
                Label121.Text = "Canceled";
                Label121.Attributes["style"] = "background-color:indigo;color:#fff;text-align:center;";
            }
            else if (ID == "aqua")
            {
                Label121.Text = "No Status";
                Label121.Attributes["style"] = "background-color:aqua;color:#fff;text-align:center;";
            }
            else
                Label121.Text = "Not Found";
        }

        protected void btnshowAPP_Click(object sender, EventArgs e)
        {
            BindAppoint(0);
        }

        protected void btnshownote_Click(object sender, EventArgs e)
        {
            Bindmemo(0);
        }

        protected void btnShowEmail_Click(object sender, EventArgs e)
        {
            BindListEmail(0);
        }
        public void ListCRMActivity()
        {
            string UNAME = ((USER_MST)Session["USER"]).LOGIN_ID.ToString();
            List<Database.CRMProgHW> CRMHW = new List<Database.CRMProgHW>();
            int MDUID = 0;
            if (Session["SiteModuleID"] != null)
            {
                MDUID = Convert.ToInt32(Session["SiteModuleID"]);
                List<Database.CRMMainActivity> MainList = DB.CRMMainActivities.Where(p => p.TenentID == TID && p.ModuleID == MDUID && p.USERNAME == UNAME).ToList();
                List<Database.CRMActivity> SubActivity = new List<Database.CRMActivity>();
                foreach (Database.CRMMainActivity Mainitem in MainList)
                {
                    if (DB.CRMActivities.Where(p => p.TenentID == TID && p.COMPID == Mainitem.COMPID && p.MasterCODE == Mainitem.MasterCODE && p.Active == "Y").Count() > 0)
                    {
                        Database.CRMActivity Sobj = DB.CRMActivities.Single(p => p.TenentID == TID && p.COMPID == Mainitem.COMPID && p.MasterCODE == Mainitem.MasterCODE && p.Active == "Y");
                        SubActivity.Add(Sobj);
                    }
                }
                foreach (Database.CRMActivity Subitem in SubActivity)
                {
                    DateTime Todaydate = DateTime.Now.Date;
                    if (DB.CRMProgHWs.Where(p => p.TenentID == TID && p.ActivityID == Subitem.ActivityID && p.RunningSerial == Subitem.LinkMasterCODE && p.ReminderDate == Todaydate).Count() > 0)
                    {
                        Database.CRMProgHW HWobj = DB.CRMProgHWs.Single(p => p.TenentID == TID && p.ActivityID == Subitem.ActivityID && p.RunningSerial == Subitem.LinkMasterCODE && p.ReminderDate == Todaydate);
                        CRMHW.Add(HWobj);

                    }
                }
            }
            ListCRMActivitis.DataSource = CRMHW;
            ListCRMActivitis.DataBind();
            if (CRMHW.Count() > 0)
            {
                CRMNoteCount.Text = CRMHW.Count().ToString();
                ClientScript.RegisterStartupScript(this.GetType(), "changeClass();", "changeClass();", true);
            }

        }
        public void ListCRMActivityAll()
        {
            string UNAME = ((USER_MST)Session["USER"]).LOGIN_ID.ToString();
            List<Database.CRMProgHW> CRMHW = new List<Database.CRMProgHW>();
            int MDUID = 0;
            if (Session["SiteModuleID"] != null)
            {
                MDUID = Convert.ToInt32(Session["SiteModuleID"]);
                List<Database.CRMMainActivity> MainList = DB.CRMMainActivities.Where(p => p.TenentID == TID && p.ModuleID == MDUID).ToList();
                List<Database.CRMActivity> SubActivity = new List<Database.CRMActivity>();
                foreach (Database.CRMMainActivity Mainitem in MainList)
                {
                    if (DB.CRMActivities.Where(p => p.TenentID == TID && p.COMPID == Mainitem.COMPID && p.MasterCODE == Mainitem.MasterCODE && p.Active == "Y" && p.USERNAME == UNAME).Count() > 0)
                    {
                        Database.CRMActivity Sobj = DB.CRMActivities.Single(p => p.TenentID == TID && p.COMPID == Mainitem.COMPID && p.MasterCODE == Mainitem.MasterCODE && p.Active == "Y" && p.USERNAME == UNAME);
                        SubActivity.Add(Sobj);
                    }
                }
                foreach (Database.CRMActivity Subitem in SubActivity)
                {
                    DateTime Todaydate = DateTime.Now.Date;
                    if (DB.CRMProgHWs.Where(p => p.TenentID == TID && p.ActivityID == Subitem.ActivityID && p.RunningSerial == Subitem.LinkMasterCODE).Count() == 1)
                    {
                        Database.CRMProgHW HWobj = DB.CRMProgHWs.Single(p => p.TenentID == TID && p.ActivityID == Subitem.ActivityID && p.RunningSerial == Subitem.LinkMasterCODE);
                        CRMHW.Add(HWobj);
                    }
                }
            }
            ListCRMActivitisAll.DataSource = CRMHW;
            ListCRMActivitisAll.DataBind();
        }
        public string GetACT(int NOTEID)
        {
            List<Database.ISActionMaster> AList = DB.ISActionMasters.Where(p => p.TenantedID == TID && p.Type == 2 && p.ActionType == 3).ToList();
            if (AList.Where(p => p.ID == NOTEID).Count() > 0)
            {
                string ACTDES = "";
                Database.ISActionMaster obj = DB.ISActionMasters.Single(p => p.TenantedID == TID && p.Type == 2 && p.ActionType == 3 && p.ID == NOTEID);
                ACTDES = obj.Title + " / " + obj.Description + " / <b>" + obj.Switch1To + "</b>";
                return ACTDES;
            }
            else
            {
                return "Something Wrong...";
            }
        }

        protected void btnTodayReminder_Click(object sender, EventArgs e)
        {
            DateTime Todaydate = DateTime.Now.Date;
            ListMemo.DataSource = DB.ISActionMasters.Where(p => p.TenantedID == TID && p.Type == 2 && p.ActionType == 3 && p.TransactionDate == Todaydate).OrderByDescending(p => p.TransactionDate);
            ListMemo.DataBind();
        }
        public void CheckCRMLead()
        {
            if(DB.tbl_Opportunity_Mst.Where(p=>p.TenentID == TID).Count() == 0)
            {
                string Opportunity = "";
                Opportunity += "INSERT INTO [dbo].[tbl_Opportunity_Mst] ([TenentID],[Conact_id],[Customer_Name],[Supplier_Name],[Project_id],[Campaign_ID],[NoteID],[TicketID],[FileID],[EmailIDAction],[MemoID],[Name],[opportunity_type],[LossReason],[Stage],[lead_source],[Amount],[AmountBackup],[AmountUSdollar],[DateClosed],[NextStep],[SalesStage],[Probability],[DateEntered],[DateModified],[CampaignName],[Description],[AccountName],[TeamName],[AssignedName],[CreatedBy],[ModifiedBy],[Active],[Deleted],[CRUP_ID],[Status],[OppName1],[OppName2],[OppName3],[TeamLeader],[RefNO],[AppointmentID]) VALUES (" + TID + ",0,'Johar','',99999,10,0,0,0,0,0,'Test',20001,200012,20003,'',0.00,'','','2018-05-31','Not Decided','12','100','2018-03-12','2018-03-12','Test','Not Decided','Johar',1240200,'johar',1,1,'true','true',0,'','Test','Test','Test',0,0,0);";
                command2 = new SqlCommand(Opportunity, con);
                con.Open();
                command2.ExecuteReader();
                con.Close();
            }
            if(DB.tbl_Campaign_Mst.Where(p=>p.TenentID == TID).Count() == 0)
            {
                string Campaign = "";
                Campaign += "INSERT INTO [dbo].[tbl_Campaign_Mst]([TenentID],[ID],[Tenet],[Name],[Name2],[Name3],[MyFavorite],[MyItems],[Compaigntype],[Status],[TypeID],[NoteID],[TicketID],[FileID],[EmailIDAction],[MemoID],[Budget],[Revenue],[TrackerText],[ReferURL],[Contents],[ContentsLang2],[ContentsLang3],[Impressions],[ActualCost],[ExpectedCost],[Objective],[Currency],[StartDate],[EndDate],[TeamName],[AssignedUser],[CreatedDate],[Active],[Deleted],[CRUP_ID],[AssignedTeamLeader],[ActualStartDate],[ActualEndDate],[CustomerSupplierSocialMediaName],[RefNO],[SearchTitle],[QuestionGroup],[AppointmentID]) VALUES (" + TID + ",10,'Test','Test','Test','Test','Developing','','300001',1,99999,0,0,0,0,0,2000.00,'','','www.erp53.com','Test','Test','Test','Design Good',0.00,2000.00,'Design','0','2016-11-22','2016-11-24',1240200,'3','2016-11-22','true','true',1,0,'2016-11-22','2016-11-24','',0,0,0,0);";
                command2 = new SqlCommand(Campaign, con);
                con.Open();
                command2.ExecuteReader();
                con.Close();
            }
            if (DB.tbl_Lead_Mst.Where(p => p.TenentID == TID).Count() == 0)
            {
                string Lead = "";
                Lead += "INSERT INTO [dbo].[tbl_Lead_Mst]([TenentID],[ID],[Conact_id],[Customer_Name],[Supplier_Name],[Project_id],[Oppertunity_ID],[Campaign_ID],[Company_ID],[Salutation],[Title],[TwitterScreenName],[Refered_by],[lead_source],[Status],[Department],[Do_not_call],[Assistant],[Assistant_phone],[email_opt_out],[invalid_email],[account_name],[opportunity_name],[opportunity_amount],[campaign_name],[date_entered],[team_name],[lead_source_description],[assigned_to_name],[created_by],[modified_by],[Active],[Deleted],[Website],[SMS_Opt_In],[CRUP_ID],[NoofEmployees],[LeadName1],[LeadName2],[LeadName3],[Email],[Name],[Address],[LeadSourcefrom],[StatusoftheLead],[PerformingDept],[TeamLeaderName],[ContactPerson],[Type],[RefNO],[QuestionGroup]) VALUES (" + TID + ",9,0,'johar','Not Known',99999,1,10,0,'1','GM','','Johar',10407,10601,'','false','Johar','12345678','true','false','','Test',0.00,'Test','2018-03-12',1240200,'Not Known','',1,1,'true','true','digital53','true',0,'','Dummy','Dummy','Dummy','','johar','Not Known','Johar',0,9,'1','Johar','',0,999999999);";
                command2 = new SqlCommand(Lead, con);
                con.Open();
                command2.ExecuteReader();
                con.Close();
            }
        }




    }

}
