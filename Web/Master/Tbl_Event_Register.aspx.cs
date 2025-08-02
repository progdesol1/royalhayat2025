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

using System.Net;
using System.IO;
using System.Diagnostics;
using System.Threading;
using System.Drawing;
using System.Data.SqlClient;
using GenCode128;

using System.Data.Objects;
using QRCoder;

namespace Web.Master
{
    public partial class Tbl_Event_Register : System.Web.UI.Page
    {
        #region Step1
        int count = 0;
        int take = 0;
        int Skip = 0;
        int TID = 0;
        string UserName;
        public static int ChoiceID = 0;

        #endregion
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
        CallEntities DB = new CallEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            UserName = ((USER_MST)Session["USER"]).FIRST_NAME.ToString();

            if (!IsPostBack)
            {
                Session["LANGUAGE"] = "en-US";
                btnAdd.ValidationGroup = "ss";
                Readonly();
                ManageLang();
                pnlSuccessMsg.Visible = false;
                FillContractorID();
                int CurrentID = 1;
                if (ViewState["Es"] != null)
                    CurrentID = Convert.ToInt32(ViewState["Es"]);
                BindData();
                Payment("none", "expand");
                //FirstData();               
            }
            if (txtAmountPaid.Text != "")
            {
                Payment("block", "collapse");
                btnsaveprint.Visible = true;
            }

        }
        #region Step2
        public void BindData()
        {

            List<Database.Tbl_Event_Register> List = DB.Tbl_Event_Register.Where(m => m.TenantID == TID).OrderBy(m => m.MyID).ToList();
            int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
            int Totalrec = List.Count();
            ((AcmMaster)Page.Master).Loadlist(Showdata, take, Skip, ChoiceID, lblShowinfEntry, btnPrevious1, btnNext1, Listview1, ListView2, Totalrec, List);
        }
        #endregion

        public void GetShow()
        {

            lblEventID1s.Attributes["class"] = lblMyID1s.Attributes["class"] = lblRegisteredAs1s.Attributes["class"] = lblRegistrationID1s.Attributes["class"] = lblPaidBy1s.Attributes["class"] = lblAmountPaid1s.Attributes["class"] = lblPaymentReference1s.Attributes["class"] = lblNotes1s.Attributes["class"] = "control-label col-md-4  getshow"; //lblMyTransID1s.Attributes["class"] = lblBatchPrintedBy1s.Attributes["class"] = lblBatchPrintedDateTime1s.Attributes["class"] = lblCertPrintedBy1s.Attributes["class"] = lblCertPrintedDateTime1s.Attributes["class"] =
            lblEventID2h.Attributes["class"] = lblMyID2h.Attributes["class"] = lblRegisteredAs2h.Attributes["class"] = lblRegistrationID2h.Attributes["class"] = lblPaidBy2h.Attributes["class"] = lblAmountPaid2h.Attributes["class"] = lblPaymentReference2h.Attributes["class"] = lblNotes2h.Attributes["class"] = "control-label col-md-4  gethide"; //lblMyTransID2h.Attributes["class"] = lblBatchPrintedBy2h.Attributes["class"] = lblBatchPrintedDateTime2h.Attributes["class"] = lblCertPrintedBy2h.Attributes["class"] = lblCertPrintedDateTime2h.Attributes["class"] = 
            b.Attributes.Remove("dir");
            b.Attributes.Add("dir", "ltr");

        }
        public void GetHide()
        {
            lblEventID1s.Attributes["class"] = lblMyID1s.Attributes["class"] = lblRegisteredAs1s.Attributes["class"] = lblRegistrationID1s.Attributes["class"] = lblPaidBy1s.Attributes["class"] = lblAmountPaid1s.Attributes["class"] = lblPaymentReference1s.Attributes["class"] = lblNotes1s.Attributes["class"] = "control-label col-md-4  gethide"; //lblMyTransID1s.Attributes["class"] = lblBatchPrintedBy1s.Attributes["class"] = lblBatchPrintedDateTime1s.Attributes["class"] = lblCertPrintedBy1s.Attributes["class"] = lblCertPrintedDateTime1s.Attributes["class"] =
            lblEventID2h.Attributes["class"] = lblMyID2h.Attributes["class"] = lblRegisteredAs2h.Attributes["class"] = lblRegistrationID2h.Attributes["class"] = lblPaidBy2h.Attributes["class"] = lblAmountPaid2h.Attributes["class"] = lblPaymentReference2h.Attributes["class"] = lblNotes2h.Attributes["class"] = "control-label col-md-4  getshow"; // lblMyTransID2h.Attributes["class"] = lblBatchPrintedBy2h.Attributes["class"] = lblBatchPrintedDateTime2h.Attributes["class"] = lblCertPrintedBy2h.Attributes["class"] = lblCertPrintedDateTime2h.Attributes["class"] =
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
            if (ViewState["MainEvenCount"] != null)
            {
                List<Database.Tbl_Event_Main> list = ((List<Database.Tbl_Event_Main>)ViewState["MainEvenCount"]);
                if (list.Count() == 1)
                    drpEventID.SelectedIndex = 1;
            }
            else
            {
                drpEventID.SelectedIndex = 0;
                txtAmountPaid.Text = "";
            }
            //txtContactMyID.Text = "";
            txtSearch.Text = "";
            txtContact.Text = "";
            txtPosition.Text = "";
            txtAttendes.Text = "";
            //updateAttendee.Update();
            txtMobile.Text = "";
            txtBussinessPhone.Text = "";
            txtEmailId.Text = "";
            txtAddress.Text = "";
            drpRegisteredAs.SelectedIndex = 0;
            drpPaydby.SelectedIndex = 0;
            txtPaymentReference.Text = "";
            //drpPaymentTermsId.SelectedIndex = 0;
            txtNotes.Text = "";
            CHKListCompany.Checked = false;
            CHKListContact.Checked = false;
            CHKListPosition.Checked = false;
            lblCompanyNames.Visible = false;
            lblContactsa.Visible = false;
            lblPositionSa.Visible = false;
            pnlComSearch.Visible = true;
            pnlContSearch.Visible = true;
            pnlPosiSearch.Visible = true;
            drpCompanyName.Items.Clear();
            drpContacts.Items.Clear();
            drpPositions.Items.Clear();
            Avatar.ImageUrl = "~/Gallery/defolt.png";

            //txtAttendedTime.Text = "";
            //txtBatchPrintedBy.Text = "";
            //txtBatchPrintedDateTime.Text = "";
            //txtCertPrintedBy.Text = "";
            //txtCertPrintedDateTime.Text = "";
            //txtRegisteredDate.Text = "";
            //txtRegisteredBy.Text = "";
            //drpCreatedBy.SelectedIndex = 0;
            //txtCreatedDate.Text = "";

        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {

            using (TransactionScope scope = new TransactionScope())
            {
                //try
                //{

                if (btnAdd.Text == "Add New")
                {

                    Write();
                    Clear();
                    btnAdd.Text = "Save";
                    btnAdd.ValidationGroup = "submit";
                }
                else if (btnAdd.Text == "Save")
                {
                    pnldanger.Visible = false;
                    if (CHKListCompany.Checked == false)
                    {
                        if (drpCompanyName.Items.Count > 0)
                        {
                            int com = Convert.ToInt32(drpCompanyName.SelectedValue);
                            if (com == 0)
                            {
                                pnldanger.Visible = true;
                                lbldmsg.Text = "Please select Company In Dropdown...";
                                return;
                            }
                        }
                        else
                        {
                            pnldanger.Visible = true;
                            lbldmsg.Text = "Please select Company In Dropdown...";
                            return;
                        }
                    }
                    if (CHKListContact.Checked == false)
                    {
                        if (drpContacts.Items.Count > 0)
                        {
                            int Con = Convert.ToInt32(drpContacts.SelectedValue);
                            if (Con == 0)
                            {
                                pnldanger.Visible = true;
                                lbldmsg.Text = "Please select Attendee In Dropdown...";
                                return;
                            }
                        }
                        else
                        {
                            pnldanger.Visible = true;
                            lbldmsg.Text = "Please select Attendee In Dropdown...";
                            return;
                        }
                    }
                    if (CHKListPosition.Checked == false)
                    {
                        if (drpPositions.Items.Count > 0)
                        {
                            int POS = Convert.ToInt32(drpPositions.SelectedValue);
                            if (POS == 0)
                            {
                                pnldanger.Visible = true;
                                lbldmsg.Text = "Please select Position In Dropdown...";
                                return;
                            }
                        }
                        else
                        {
                            pnldanger.Visible = true;
                            lbldmsg.Text = "Please select Position In Dropdown...";
                            return;
                        }
                    }
                    //validate
                    int tcontact = 0;
                    bool flagg = false;
                    if (CHKListContact.Checked == false)
                    {
                        string[] AttendeeName = drpContacts.SelectedItem.ToString().Split('-');
                        string ATTName = AttendeeName[0];
                        int AttTenent = Convert.ToInt32(AttendeeName[1]);
                        if (AttTenent != TID)
                        {
                            int TC = Convert.ToInt32(drpContacts.SelectedValue);
                            tcontact = Contacall(AttTenent, TC);
                            flagg = true;
                        }
                    }
                    int COMPID = 0;
                    bool Comflage = false;
                    if (CHKListCompany.Checked == false)
                    {
                        string[] CompnameAR = drpCompanyName.SelectedItem.ToString().Trim().Split('-');
                        string CompNAme = CompnameAR[0];
                        int CompTenent = Convert.ToInt32(CompnameAR[1]);
                        if (CompTenent != TID)
                        {
                            int ComID = Convert.ToInt32(drpCompanyName.SelectedValue);
                            COMPID = companyall(CompTenent, ComID);
                            Comflage = true;
                        }

                    }
                    List<Database.Tbl_Event_Register> List = DB.Tbl_Event_Register.Where(p => p.TenantID == TID).ToList();
                    Database.Tbl_Event_Register objTbl_Event_Register = new Database.Tbl_Event_Register();
                    //Server Content Send data Yogesh
                    objTbl_Event_Register.TenantID = TID;
                    int MainEventID = Convert.ToInt32(drpEventID.SelectedValue);
                    int SubEventID = Convert.ToInt32(drpMyID.SelectedValue);
                    int ContactMyidd = Convert.ToInt32(drpContacts.SelectedValue);
                    if (List.Where(p => p.EventID == MainEventID && p.MyID == SubEventID && p.ContactMyID == ContactMyidd).Count() > 0)
                    {
                        pnldanger.Visible = true;
                        lbldmsg.Text = "This Attendee Registration Is AllReady Exist...";
                        return;
                    }
                    objTbl_Event_Register.EventID = MainEventID;
                    objTbl_Event_Register.MyID = SubEventID;

                    //objTbl_Event_Register.ContactMyID = DB.Tbl_Event_Register.Where(p=>p.TenantID == TID && p.EventID == MainEventID && p.MyID == SubMyID).Count()>0? Convert.ToInt32(DB.Tbl_Event_Register.Where(p=>p.TenantID == TID && p.EventID == MainEventID && p.MyID == SubMyID).Max(p=>p.ContactMyID)+1):1; //Convert.ToInt32(txtContactMyID.Text);
                    objTbl_Event_Register.RegisteredAs = Convert.ToInt32(drpRegisteredAs.SelectedValue);
                    objTbl_Event_Register.RegistrationID = txtRegistrationID.Text;
                    //objTbl_Event_Register.MyTransID = Convert.ToInt32(drpMyTransID.SelectedValue);

                    if (CHKListCompany.Checked == true)
                        objTbl_Event_Register.CompanyId = 20;
                    else
                    {
                        if (Comflage == true)//COMPID
                            objTbl_Event_Register.CompanyId = Convert.ToInt32(COMPID);
                        else
                            objTbl_Event_Register.CompanyId = Convert.ToInt32(drpCompanyName.SelectedValue);
                    }

                    if (CHKListCompany.Checked == true)
                        objTbl_Event_Register.CompanyName = txtSearch.Text;
                    else
                    {
                        string[] Cname = drpCompanyName.SelectedItem.ToString().Trim().Split('-');
                        string Cnamef = Cname[0];
                        objTbl_Event_Register.CompanyName = Cnamef;
                    }

                    if (CHKListContact.Checked == true)
                        objTbl_Event_Register.ContactMyID = DB.Tbl_Event_Register.Where(p => p.TenantID == TID && p.EventID == MainEventID && p.MyID == SubEventID && p.ContactMyID >= 99999).Count() > 0 ? Convert.ToInt32(DB.Tbl_Event_Register.Where(p => p.TenantID == TID && p.EventID == MainEventID && p.MyID == SubEventID && p.ContactMyID >= 99999).Max(p => p.ContactMyID) + 1) : 99999;
                    else
                    {
                        if (flagg == true)//tcontact
                            objTbl_Event_Register.ContactMyID = Convert.ToInt32(tcontact);
                        else
                            objTbl_Event_Register.ContactMyID = Convert.ToInt32(drpContacts.SelectedValue);
                    }

                    if (CHKListContact.Checked == true)
                        objTbl_Event_Register.Attendee = txtContact.Text;
                    else
                        objTbl_Event_Register.Attendee = txtAttendes.Text;//drpContacts.SelectedItem.ToString();

                    if (CHKListPosition.Checked == true)
                        objTbl_Event_Register.PositionName = txtPosition.Text;
                    else
                        objTbl_Event_Register.PositionName = drpPositions.SelectedItem.ToString();

                    if (CHKListPosition.Checked == true)
                        objTbl_Event_Register.positionId = 99999;
                    else
                        objTbl_Event_Register.positionId = Convert.ToInt32(drpPositions.SelectedValue);

                    if (FileUpload1.HasFile)
                    {
                        string path = FileUpload1.FileName;
                        FileUpload1.SaveAs(Server.MapPath("~/Master/Img/" + path));
                        objTbl_Event_Register.IMG = path;
                    }

                    objTbl_Event_Register.MobileNo = txtMobile.Text;
                    objTbl_Event_Register.Busphone = txtBussinessPhone.Text;
                    objTbl_Event_Register.Email = txtEmailId.Text;
                    objTbl_Event_Register.Address = txtAddress.Text;
                    objTbl_Event_Register.PaidBy = Convert.ToInt32(drpPaydby.SelectedValue);
                    objTbl_Event_Register.AmountPaid = Convert.ToDecimal(txtAmountPaid.Text);
                    objTbl_Event_Register.PaymentReference = txtPaymentReference.Text;
                    objTbl_Event_Register.Notes = txtNotes.Text;
                    //objTbl_Event_Register.PaymentTermsId = Convert.ToInt32(drpPaymentTermsId.SelectedValue);                                
                    //objTbl_Event_Register.AttendedTime = Convert.ToDateTime(txtAttendedTime.Text);
                    objTbl_Event_Register.BatchPrintedBy = UserName;
                    objTbl_Event_Register.BatchPrintedDateTime = DateTime.Now;
                    objTbl_Event_Register.CertPrintedBy = UserName;
                    objTbl_Event_Register.CertPrintedDateTime = DateTime.Now;
                    objTbl_Event_Register.RegisteredBy = UserName;
                    objTbl_Event_Register.RegisteredDate = DateTime.Now;
                    objTbl_Event_Register.CreatedBy = 1;
                    objTbl_Event_Register.CreatedDate = DateTime.Now;
                    objTbl_Event_Register.BARCODE = TID.ToString() + "" + MainEventID.ToString() + "" + SubEventID.ToString() + "" + objTbl_Event_Register.ContactMyID.ToString();
                    ViewState["Contact"] = objTbl_Event_Register.ContactMyID;
                    ViewState["COM"] = MainEventID;
                    ViewState["CON"] = SubEventID;

                    //Insert ICTR_HD
                    int MYTRANSID = DB.ICTR_HD.Where(p => p.TenentID == TID).Count() > 0 ? Convert.ToInt32(DB.ICTR_HD.Where(p => p.TenentID == TID).Max(p => p.MYTRANSID)) + 1 : 1;
                    objTbl_Event_Register.PaymentTermsId = MYTRANSID;

                    decimal compid = 0;
                    string SystemName = "EVN";
                    string MainTranType = "";
                    string TransType = "Event Payment In";
                    int transid = 7101;
                    int transsubid = 710101;
                    if (drpCompanyName.SelectedValue == "" || drpCompanyName.SelectedValue == "0")
                        compid = 20;
                    else
                        compid = Convert.ToDecimal(drpCompanyName.SelectedValue);

                    int compid1 = 0;
                    if (drpCompanyName.SelectedValue == "" || drpCompanyName.SelectedValue == "0")
                        compid1 = 20;
                    else
                        compid1 = Convert.ToInt32(drpCompanyName.SelectedValue);

                    string PERIOD_CODE = "201802";
                    DateTime TRANSDATE = DateTime.Now;
                    string REFERENCE = txtPaymentReference.Text;
                    string NOTES = txtNotes.Text;
                    string USERID = ((USER_MST)Session["USER"]).FIRST_NAME;
                    bool Active = true;
                    string DatainserStatest = "Add";

                    Classes.EcommAdminClass.insertICTR_HD(TID, MYTRANSID, 1, 1, MainTranType, TransType, transid, transsubid, SystemName, compid, compid, "L", PERIOD_CODE, "0", 0, "0", 0, 0, 0, "0", "0", "0", 0, TRANSDATE, REFERENCE, NOTES, 0, "1", "1", "1", "1", "1", "1", USERID, Active, compid1, TRANSDATE, TRANSDATE, TRANSDATE, 0, 0, "DE", 0, DatainserStatest, "0", "0", "0", 0, "Event", "0", 0);

                    //Insert ICTRPayTerms_HD
                    int PaymentTermsId = Convert.ToInt32(drpPaydby.SelectedValue);
                    string CounterID = "0";
                    int LID = 1;
                    decimal Amount = Convert.ToDecimal(txtAmountPaid.Text);
                    string IDRefresh = txtPaymentReference.Text;
                    Classes.EcommAdminClass.insertICTRPayTerms_HD(TID, MYTRANSID, PaymentTermsId, CounterID, LID, 0, Amount, IDRefresh, null, null, null, 0, 0, "1", 0, false, false, false, false, null, null, null, null, null);

                    DB.Tbl_Event_Register.AddObject(objTbl_Event_Register);
                    DB.SaveChanges();
                    Clear();
                    //lblMsg.Text = "Data Save Successfully";
                    //pnlSuccessMsg.Visible = true;
                    btnAdd.Text = "Add New";
                    drpMyID.Items.Clear();
                    btnAdd.ValidationGroup = "ss";
                    BindData();
                    Readonly();
                    Classes.Toastr.ShowToast(Page, Classes.Toastr.ToastType.Success, "Data Save Successfully", "Success!", Classes.Toastr.ToastPosition.TopCenter);
                    //FirstData();
                }
                else if (btnAdd.Text == "Update")
                {
                    if (ViewState["MainEveID"] != null && ViewState["myID"] != null && ViewState["ContactMYID"] != null)
                    {
                        int MEventID = Convert.ToInt32(ViewState["MainEveID"]);
                        int myID = Convert.ToInt32(ViewState["myID"]);
                        int ContactMYID = Convert.ToInt32(ViewState["ContactMYID"]);
                        Database.Tbl_Event_Register objTbl_Event_Register = DB.Tbl_Event_Register.Single(p => p.TenantID == TID && p.EventID == MEventID && p.MyID == myID && p.ContactMyID == ContactMYID);
                        //objTbl_Event_Register.EventID = Convert.ToInt32(drpEventID.SelectedValue);
                        //objTbl_Event_Register.MyID = Convert.ToInt32(drpMyID.SelectedValue);
                        //objTbl_Event_Register.ContactMyID = Convert.ToDecimal(txtContactMyID.Text);
                        objTbl_Event_Register.RegisteredAs = Convert.ToInt32(drpRegisteredAs.SelectedValue);
                        objTbl_Event_Register.RegistrationID = txtRegistrationID.Text;

                        objTbl_Event_Register.Attendee = txtContact.Text;
                        objTbl_Event_Register.CompanyName = txtSearch.Text;
                        objTbl_Event_Register.PositionName = txtPosition.Text;
                        if (FileUpload1.HasFile)
                        {
                            string path = FileUpload1.FileName;
                            FileUpload1.SaveAs(Server.MapPath("~/Master/Img/" + path));
                            objTbl_Event_Register.IMG = path;
                        }
                        objTbl_Event_Register.MobileNo = txtMobile.Text;
                        objTbl_Event_Register.Email = txtEmailId.Text;
                        objTbl_Event_Register.Busphone = txtBussinessPhone.Text;
                        objTbl_Event_Register.Address = txtAddress.Text;
                        objTbl_Event_Register.PaidBy = Convert.ToInt32(drpPaydby.SelectedValue);
                        objTbl_Event_Register.AmountPaid = Convert.ToDecimal(txtAmountPaid.Text);
                        objTbl_Event_Register.PaymentReference = txtPaymentReference.Text;
                        objTbl_Event_Register.Notes = txtNotes.Text;

                        int MYTRANSID = Convert.ToInt32(objTbl_Event_Register.PaymentTermsId);
                        int PaymentTermsId = Convert.ToInt32(drpPaydby.SelectedValue);
                        string CounterID = "0";
                        int LID = 1;
                        decimal Amount = Convert.ToDecimal(txtAmountPaid.Text);
                        string IDRefresh = txtPaymentReference.Text;
                        Classes.EcommAdminClass.insertICTRPayTerms_HD(TID, MYTRANSID, PaymentTermsId, CounterID, LID, 0, Amount, IDRefresh, null, null, null, 0, 0, "1", 0, false, false, false, false, null, null, null, null, null);

                        ViewState["MainEveID"] = null;
                        ViewState["myID"] = null;
                        ViewState["ContactMYID"] = null;
                        btnAdd.Text = "Add New";
                        btnAdd.ValidationGroup = "ss";
                        drpMyID.Items.Clear();
                        DB.SaveChanges();
                        Clear();
                        //lblMsg.Text = "Data Edit Successfully";
                        //pnlSuccessMsg.Visible = true;
                        BindData();
                        Readonly();
                        Classes.Toastr.ShowToast(Page, Classes.Toastr.ToastType.Success, "Data Edit Successfully", "Success!", Classes.Toastr.ToastPosition.TopCenter);
                        //FirstData();
                    }
                }
                BindData();

                scope.Complete(); //  To commit.                   

            }
            //barcode generater 2
            try
            {
                if (ViewState["Contact"] != null && ViewState["COM"] != null && ViewState["CON"] != null)
                {
                    int Contactmyod = Convert.ToInt32(ViewState["Contact"]);
                    int MainEventID = Convert.ToInt32(ViewState["COM"]);
                    int SubEventID = Convert.ToInt32(ViewState["CON"]);
                    var List = DB.Tbl_Event_Register.Where(p => p.TenantID == TID && p.EventID == MainEventID && p.MyID == SubEventID && p.ContactMyID == Contactmyod).ToList();
                    foreach (Database.Tbl_Event_Register item in List)
                    {
                        string str = Server.MapPath("~/Master/Img/" + item.BARCODE.Trim() + ".png");
                        System.Drawing.Image myimg = MakeBarcodeImage(item.BARCODE.Trim(), 4, true);
                        string barCode = item.BARCODE.Trim();
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
                                        File.WriteAllBytes(Server.MapPath("~/Master/Img/" + barCode + ".png"), byteImage);
                                    }
                                    catch (System.Exception ex)
                                    {
                                        Response.Write(ex.InnerException);
                                    }
                                }
                            }
                        }
                        showBarcode(barCode);
                        Classes.Toastr.ShowToast(Page, Classes.Toastr.ToastType.Success, "Barcode genarate Successfully", "Barcode genarate 1", Classes.Toastr.ToastPosition.BottomLeft);
                    }

                    ViewState["Contact"] = null;
                    ViewState["COM"] = null;
                    ViewState["CON"] = null;
                }
            }
            catch (Exception ex)
            {
                Classes.Toastr.ShowToast(Page, Classes.Toastr.ToastType.Error, "Error Occured During Barcode genarate 1 process !<br>" + ex.ToString(), "Barcode genarate 2", Classes.Toastr.ToastPosition.TopCenter);
            }
        }
        public System.Drawing.Image MakeBarcodeImage(string InputData, int BarWeight, bool AddQuietZone)
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

            Image1.ImageUrl = "~/Master/Img/" + Bar + ".png";
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
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            //Response.Redirect(Session["Previous"].ToString());D:\NewSaas\Web\Master\Tbl_Event_Register.aspx
            Response.Redirect("Tbl_Event_Register.aspx");
        }
        protected void drpContacts_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillAttendee();
        }
        public void FillAttendee()
        {

            int CONID = Convert.ToInt32(drpContacts.SelectedValue);
            if (CONID != 0)
            {
                string[] AttendeeName = drpContacts.SelectedItem.ToString().Split('-');
                string ATTName = AttendeeName[0];
                int AttTenent = Convert.ToInt32(AttendeeName[1]);
                txtAttendes.Text = ATTName;
                txtContact.Text = ATTName;
                txtRegistrationID.Text = CONID.ToString();
                if (DB.TBLCONTACTs.Where(p => p.TenentID == AttTenent && p.ContactMyID == CONID).Count() > 0)
                {
                    Database.TBLCONTACT obj = DB.TBLCONTACTs.Single(p => p.TenentID == AttTenent && p.ContactMyID == CONID);
                    if (txtMobile.Text != "")
                        txtMobile.Text += obj.MOBPHONE != null && obj.MOBPHONE != "" ? "," + obj.MOBPHONE.ToString() : "";
                    else
                        txtMobile.Text = obj.MOBPHONE != null && obj.MOBPHONE != "" ? obj.MOBPHONE.ToString() : "";
                    if (txtBussinessPhone.Text != "")
                        txtBussinessPhone.Text += obj.BUSPHONE1 != null && obj.BUSPHONE1 != "" ? "," + obj.BUSPHONE1.ToString() : "";
                    else
                        txtBussinessPhone.Text = obj.BUSPHONE1 != null && obj.BUSPHONE1 != "" ? obj.BUSPHONE1.ToString() : "";
                    if (txtAddress.Text != "")
                        txtAddress.Text += obj.ADDR1 != null && obj.ADDR1 != "" ? "," + obj.ADDR1.ToString() : "";
                    else
                        txtAddress.Text = obj.ADDR1 != null && obj.ADDR1 != "" ? obj.ADDR1.ToString() : "";
                    if (txtEmailId.Text != "")
                        txtEmailId.Text += obj.EMAIL1 != null && obj.EMAIL1 != "" ? "," + obj.EMAIL1.ToString() : "";
                    else
                        txtEmailId.Text = obj.EMAIL1 != null && obj.EMAIL1 != "" ? obj.EMAIL1.ToString() : "";
                }
            }
            //else
            //{
            //    int CONID = Convert.ToInt32(drpContacts.SelectedValue);
            //    string AttendeeName = drpContacts.SelectedItem.ToString();
            //    txtAttendes.Text = AttendeeName;
            //    //updateAttendee.Update();
            //    txtContact.Text = AttendeeName;

            //    string ConRegID = drpContacts.SelectedValue.ToString();
            //    txtRegistrationID.Text = ConRegID;
            //    //UpdateRegistration.Update();

            //    if (DB.TBLCONTACTs.Where(p => p.TenentID == TID && p.ContactMyID == CONID).Count() > 0)
            //    {
            //        Database.TBLCONTACT obj = DB.TBLCONTACTs.Single(p => p.TenentID == TID && p.ContactMyID == CONID);
            //        if (txtMobile.Text != "")
            //            txtMobile.Text += obj.MOBPHONE != null && obj.MOBPHONE != "" ? "," + obj.MOBPHONE.ToString() : "";
            //        else
            //            txtMobile.Text = obj.MOBPHONE != null && obj.MOBPHONE != "" ? obj.MOBPHONE.ToString() : "";
            //        if (txtBussinessPhone.Text != "")
            //            txtBussinessPhone.Text += obj.BUSPHONE1 != null && obj.BUSPHONE1 != "" ? "," + obj.BUSPHONE1.ToString() : "";
            //        else
            //            txtBussinessPhone.Text = obj.BUSPHONE1 != null && obj.BUSPHONE1 != "" ? obj.BUSPHONE1.ToString() : "";
            //        if (txtAddress.Text != "")
            //            txtAddress.Text += obj.ADDR1 != null && obj.ADDR1 != "" ? "," + obj.ADDR1.ToString() : "";
            //        else
            //            txtAddress.Text = obj.ADDR1 != null && obj.ADDR1 != "" ? obj.ADDR1.ToString() : "";
            //        if (txtEmailId.Text != "")
            //            txtEmailId.Text += obj.EMAIL1 != null && obj.EMAIL1 != "" ? "," + obj.EMAIL1.ToString() : "";
            //        else
            //            txtEmailId.Text = obj.EMAIL1 != null && obj.EMAIL1 != "" ? obj.EMAIL1.ToString() : "";
            //    }
            //}
        }
        public void FillContractorID()
        {
            var MDT = DateTime.Now.Date;
            List<Database.Tbl_Event_Main> MainEvenList = DB.Tbl_Event_Main.Where(p => p.TenantID == TID).ToList();
            List<Database.tbl_Event_Detail> DetailEventList = DB.tbl_Event_Detail.Where(p => p.TenantID == TID).ToList();
            if (Request.QueryString["SEID"] != null && Request.QueryString["MEID"] != null)
            {
                
                int myID = Convert.ToInt32(Request.QueryString["SEID"]);
                int EventID = Convert.ToInt32(Request.QueryString["MEID"]);

                drpEventID.DataSource = DB.Tbl_Event_Main.Where(p => p.TenantID == TID && p.EventID == EventID && p.Activated == true && p.ToDate >= MDT);
                drpEventID.DataTextField = "Title2Print";
                drpEventID.DataValueField = "EventID";
                drpEventID.DataBind();
                //drpEventID.Items.Insert(0, new ListItem("--Select--", "0"));

                drpMyID.DataSource = DB.tbl_Event_Detail.Where(p => p.TenantID == TID && p.EventID == EventID && p.MyID == myID && p.Activated == true);
                drpMyID.DataTextField = "Description1";
                drpMyID.DataValueField = "MyID";
                drpMyID.DataBind();
                //drpMyID.Items.Insert(0, new ListItem("--Select--", "0"));

                Database.tbl_Event_Detail OBJDetail = DB.tbl_Event_Detail.Single(p => p.TenantID == TID && p.EventID == EventID && p.MyID == myID && p.Activated == true);
                string regID = OBJDetail.RegisterationIDBegin.ToString();
                string regNO = OBJDetail.RegisterationNumber.ToString();
                string RegConcate = regID + "" + regNO;
                txtRegistrationID.Text = RegConcate;
                //UpdateRegistration.Update();
                txtAmountPaid.Text = OBJDetail.Rate.ToString();
                rngAmount.MaximumValue = txtAmountPaid.Text;

                string Ename = drpEventID.SelectedItem.ToString();
                string Evalue = drpEventID.SelectedValue.ToString();
                string Sname = drpMyID.SelectedItem.ToString();
                string svalue = drpMyID.SelectedValue.ToString();
                TextBox2.Text = svalue + "-" + Sname;
                TextBox1.Text = Evalue + "-" + Ename;

                Write();
                DrpRead();
                btnAdd.Text = "Save";
            }
            else if (ViewState["MainEveID"] != null && ViewState["myID"] != null)
            {
                pnlSuccessMsg.Visible = false;
                int MainEveID = Convert.ToInt32(ViewState["MainEveID"]);
                int myID = Convert.ToInt32(ViewState["myID"]);
                //drpEventID.DataSource = DB.Tbl_Event_Main.Where(p => p.TenantID == TID && p.ToDate >= DateTime.Now && p.EventID == MainEveID && p.Activated == true);
                //drpEventID.DataTextField = "Title2Print";
                //drpEventID.DataValueField = "EventID";
                //drpEventID.DataBind();
                //drpEventID.SelectedValue = MainEveID.ToString();

                //drpMyID.DataSource = DB.tbl_Event_Detail.Where(p => p.TenantID == TID && p.EventID == MainEveID && p.MyID == myID && p.Activated == true);
                //drpMyID.DataTextField = "Description1";
                //drpMyID.DataValueField = "MyID";
                //drpMyID.DataBind();
                //drpMyID.SelectedValue = myID.ToString();
                ////ID = evn.EventID +","+ evn.MyID
                if (DB.View_EventMainDetail.Where(p => p.TenantID == TID && p.EventID == MainEveID && p.MyID == myID && p.ToDate >= MDT).Count() > 0)
                {
                    var EList = from evn in DB.View_EventMainDetail.Where(p => p.TenantID == TID && p.ToDate >= MDT).ToList()
                            select new
                            {
                                Name = evn.Title2Print + " - " + evn.Description1,
                                ID = evn.EventID + "," + evn.MyID
                            };
                drpMD.DataSource = EList;
                drpMD.DataTextField = "Name";
                drpMD.DataValueField = "ID";
                drpMD.DataBind();
                drpMD.Items.Insert(0, new ListItem("--Select--", "0"));
                if (EList.Count() == 1)
                {
                    drpMD.SelectedIndex = 1;
                    MDfun();
                }
                drpMD.SelectedValue = MainEveID.ToString() + "," + myID.ToString();
                }
                else
                {
                    pnlSuccessMsg.Visible = true;
                    lblMsg.Text = "This Event is expired";
                }
            }
            else
            {
                List<Database.Tbl_Event_Main> MainEvenCount = MainEvenList.Where(p => p.TenantID == TID && p.ToDate >= MDT && p.Activated == true).ToList();
                drpEventID.DataSource = MainEvenList.Where(p => p.TenantID == TID && p.Activated == true);
                drpEventID.DataTextField = "Title2Print";
                drpEventID.DataValueField = "EventID";
                drpEventID.DataBind();
                drpEventID.Items.Insert(0, new ListItem("--Select--", "0"));
                if (MainEvenCount.Count() == 1)
                {
                    drpEventID.SelectedIndex = 1;
                    ViewState["MainEvenCount"] = MainEvenCount;
                    int MainEventID = Convert.ToInt32(drpEventID.SelectedValue);
                    List<Database.tbl_Event_Detail> DetailEventCount = DetailEventList.Where(p => p.TenantID == TID && p.EventID == MainEventID && p.Activated == true).ToList();
                    drpMyID.DataSource = DetailEventList.Where(p => p.TenantID == TID && p.EventID == MainEventID && p.Activated == true);
                    drpMyID.DataTextField = "Description1";
                    drpMyID.DataValueField = "MyID";
                    drpMyID.DataBind();
                    drpMyID.Items.Insert(0, new ListItem("--Select--", "0"));
                    if (DetailEventCount.Count() == 1)
                    {
                        drpMyID.SelectedIndex = 1;
                        ViewState["DetailEventCount"] = DetailEventCount;
                        registrationID();
                        SubDetail();
                    }
                }
                //drpMD
                var EList = from evn in DB.View_EventMainDetail.Where(p => p.TenantID == TID && p.ToDate >= MDT).ToList()
                            select new
                            {
                                Name = evn.Title2Print + " - " + evn.Description1,
                                ID = evn.EventID + "," + evn.MyID
                            };
                drpMD.DataSource = EList;
                drpMD.DataTextField = "Name";
                drpMD.DataValueField = "ID";
                drpMD.DataBind();
                drpMD.Items.Insert(0, new ListItem("--Select--", "0"));
                if (EList.Count() == 1)
                {
                    drpMD.SelectedIndex = 1;
                    MDfun();
                }

            }
            drpRegisteredAs.DataSource = DB.REFTABLEs.Where(p => p.TenentID == TID && p.REFTYPE == "EVN" && p.REFSUBTYPE == "RegisterType").OrderBy(p => p.SWITCH4);
            drpRegisteredAs.DataTextField = "REFNAME1";
            drpRegisteredAs.DataValueField = "REFID";
            drpRegisteredAs.DataBind();
            drpRegisteredAs.Items.Insert(0, new ListItem("--Select--", "0"));

            drpPaydby.DataSource = DB.REFTABLEs.Where(p => p.TenentID == TID && p.REFTYPE == "Payment" && p.REFSUBTYPE == "Method" && p.SHORTNAME == "Event");
            drpPaydby.DataTextField = "REFNAME1";
            drpPaydby.DataValueField = "REFID";
            drpPaydby.DataBind();
            drpPaydby.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        protected void drpMD_SelectedIndexChanged(object sender, EventArgs e)
        {
            MDfun();
        }
        public void MDfun()
        {
            string ID = drpMD.SelectedValue.ToString();
            string[] IID = ID.ToString().Split(',');
            string EID = (IID[0]).ToString();
            int EIDD = Convert.ToInt32(EID);
            string DID = (IID[1]).ToString();
            drpEventID.SelectedValue = EID;
            drpMyID.DataSource = DB.tbl_Event_Detail.Where(p => p.TenantID == TID && p.EventID == EIDD && p.Activated == true);
            drpMyID.DataTextField = "Description1";
            drpMyID.DataValueField = "MyID";
            drpMyID.DataBind();
            drpMyID.Items.Insert(0, new ListItem("--Select--", "0"));
            drpMyID.SelectedValue = DID;
            registrationID();
            SubDetail();
        }
        protected void drpEventID_SelectedIndexChanged(object sender, EventArgs e)
        {
            int MainEventID = Convert.ToInt32(drpEventID.SelectedValue);
            drpMyID.DataSource = DB.tbl_Event_Detail.Where(p => p.TenantID == TID && p.EventID == MainEventID && p.Activated == true);
            drpMyID.DataTextField = "Description1";
            drpMyID.DataValueField = "MyID";
            drpMyID.DataBind();
            drpMyID.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        protected void drpMyID_SelectedIndexChanged(object sender, EventArgs e)
        {
            registrationID();
            SubDetail();
        }
        public void SubDetail()
        {
            int MainEventID = Convert.ToInt32(drpEventID.SelectedValue);
            int SubEventID = Convert.ToInt32(drpMyID.SelectedValue);
            Database.tbl_Event_Detail OBJDetail = DB.tbl_Event_Detail.Single(p => p.TenantID == TID && p.EventID == MainEventID && p.MyID == SubEventID);
            txtAmountPaid.Text = OBJDetail.Rate.ToString();
            rngAmount.MaximumValue = txtAmountPaid.Text;

            string Ename = drpEventID.SelectedItem.ToString();
            string Evalue = drpEventID.SelectedValue.ToString();
            string Sname = drpMyID.SelectedItem.ToString();
            string svalue = drpMyID.SelectedValue.ToString();
            TextBox2.Text = svalue + "-" + Sname;
            TextBox1.Text = Evalue + "-" + Ename;
            if (txtAmountPaid.Text != "")
            {
                Payment("block", "collapse");
                btnsaveprint.Visible = true;
            }

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
            drpEventID.SelectedValue = Listview1.SelectedDataKey[0].ToString();
            drpMyID.SelectedValue = Listview1.SelectedDataKey[0].ToString();
            //txtContactMyID.Text = Listview1.SelectedDataKey[0].ToString();
            drpRegisteredAs.SelectedValue = Listview1.SelectedDataKey[0].ToString();
            txtRegistrationID.Text = Listview1.SelectedDataKey[0].ToString();
            //drpMyTransID.SelectedValue = Listview1.SelectedDataKey[0].ToString();
            //drpPaidBy.SelectedValue = Listview1.SelectedDataKey[0].ToString();
            txtAmountPaid.Text = Listview1.SelectedDataKey[0].ToString();
            txtPaymentReference.Text = Listview1.SelectedDataKey[0].ToString();
            //drpPaymentTermsId.SelectedValue = Listview1.SelectedDataKey[0].ToString();
            txtNotes.Text = Listview1.SelectedDataKey[0].ToString();
            //txtAttendedTime.Text = Listview1.SelectedDataKey[0].ToString();
            //txtBatchPrintedBy.Text = Listview1.SelectedDataKey[0].ToString();
            //txtBatchPrintedDateTime.Text = Listview1.SelectedDataKey[0].ToString();
            //txtCertPrintedBy.Text = Listview1.SelectedDataKey[0].ToString();
            //txtCertPrintedDateTime.Text = Listview1.SelectedDataKey[0].ToString();
            //txtRegisteredDate.Text = Listview1.SelectedDataKey[0].ToString();
            //txtRegisteredBy.Text = Listview1.SelectedDataKey[0].ToString();
            //drpCreatedBy.SelectedValue = Listview1.SelectedDataKey[0].ToString();
            //txtCreatedDate.Text = Listview1.SelectedDataKey[0].ToString();

        }
        public void NextData()
        {

            if (Listview1.SelectedIndex != Listview1.Items.Count - 1)
            {
                Listview1.SelectedIndex = Listview1.SelectedIndex + 1;
                drpEventID.SelectedValue = Listview1.SelectedDataKey[0].ToString();
                drpMyID.SelectedValue = Listview1.SelectedDataKey[0].ToString();
                //txtContactMyID.Text = Listview1.SelectedDataKey[0].ToString();
                drpRegisteredAs.SelectedValue = Listview1.SelectedDataKey[0].ToString();
                txtRegistrationID.Text = Listview1.SelectedDataKey[0].ToString();
                //drpMyTransID.SelectedValue = Listview1.SelectedDataKey[0].ToString();
                //drpPaidBy.SelectedValue = Listview1.SelectedDataKey[0].ToString();
                txtAmountPaid.Text = Listview1.SelectedDataKey[0].ToString();
                txtPaymentReference.Text = Listview1.SelectedDataKey[0].ToString();
                //drpPaymentTermsId.SelectedValue = Listview1.SelectedDataKey[0].ToString();
                txtNotes.Text = Listview1.SelectedDataKey[0].ToString();
                //txtAttendedTime.Text = Listview1.SelectedDataKey[0].ToString();
                //txtBatchPrintedBy.Text = Listview1.SelectedDataKey[0].ToString();
                //txtBatchPrintedDateTime.Text = Listview1.SelectedDataKey[0].ToString();
                //txtCertPrintedBy.Text = Listview1.SelectedDataKey[0].ToString();
                //txtCertPrintedDateTime.Text = Listview1.SelectedDataKey[0].ToString();
                //txtRegisteredDate.Text = Listview1.SelectedDataKey[0].ToString();
                //txtRegisteredBy.Text = Listview1.SelectedDataKey[0].ToString();
                //drpCreatedBy.SelectedValue = Listview1.SelectedDataKey[0].ToString();
                //txtCreatedDate.Text = Listview1.SelectedDataKey[0].ToString();

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
                drpEventID.SelectedValue = Listview1.SelectedDataKey[0].ToString();
                drpMyID.SelectedValue = Listview1.SelectedDataKey[0].ToString();
                //txtContactMyID.Text = Listview1.SelectedDataKey[0].ToString();
                drpRegisteredAs.SelectedValue = Listview1.SelectedDataKey[0].ToString();
                txtRegistrationID.Text = Listview1.SelectedDataKey[0].ToString();
                //drpMyTransID.SelectedValue = Listview1.SelectedDataKey[0].ToString();
                //drpPaidBy.SelectedValue = Listview1.SelectedDataKey[0].ToString();
                txtAmountPaid.Text = Listview1.SelectedDataKey[0].ToString();
                txtPaymentReference.Text = Listview1.SelectedDataKey[0].ToString();
                //drpPaymentTermsId.SelectedValue = Listview1.SelectedDataKey[0].ToString();
                txtNotes.Text = Listview1.SelectedDataKey[0].ToString();
                //txtAttendedTime.Text = Listview1.SelectedDataKey[0].ToString();
                //txtBatchPrintedBy.Text = Listview1.SelectedDataKey[0].ToString();
                //txtBatchPrintedDateTime.Text = Listview1.SelectedDataKey[0].ToString();
                //txtCertPrintedBy.Text = Listview1.SelectedDataKey[0].ToString();
                //txtCertPrintedDateTime.Text = Listview1.SelectedDataKey[0].ToString();
                //txtRegisteredDate.Text = Listview1.SelectedDataKey[0].ToString();
                //txtRegisteredBy.Text = Listview1.SelectedDataKey[0].ToString();
                //drpCreatedBy.SelectedValue = Listview1.SelectedDataKey[0].ToString();
                //txtCreatedDate.Text = Listview1.SelectedDataKey[0].ToString();

            }
        }
        public void LastData()
        {
            Listview1.SelectedIndex = Listview1.Items.Count - 1;
            drpEventID.SelectedValue = Listview1.SelectedDataKey[0].ToString();
            drpMyID.SelectedValue = Listview1.SelectedDataKey[0].ToString();
            //txtContactMyID.Text = Listview1.SelectedDataKey[0].ToString();
            drpRegisteredAs.SelectedValue = Listview1.SelectedDataKey[0].ToString();
            txtRegistrationID.Text = Listview1.SelectedDataKey[0].ToString();
            //drpMyTransID.SelectedValue = Listview1.SelectedDataKey[0].ToString();
            //drpPaidBy.SelectedValue = Listview1.SelectedDataKey[0].ToString();
            txtAmountPaid.Text = Listview1.SelectedDataKey[0].ToString();
            txtPaymentReference.Text = Listview1.SelectedDataKey[0].ToString();
            //drpPaymentTermsId.SelectedValue = Listview1.SelectedDataKey[0].ToString();
            txtNotes.Text = Listview1.SelectedDataKey[0].ToString();
            //txtAttendedTime.Text = Listview1.SelectedDataKey[0].ToString();
            //txtBatchPrintedBy.Text = Listview1.SelectedDataKey[0].ToString();
            //txtBatchPrintedDateTime.Text = Listview1.SelectedDataKey[0].ToString();
            //txtCertPrintedBy.Text = Listview1.SelectedDataKey[0].ToString();
            //txtCertPrintedDateTime.Text = Listview1.SelectedDataKey[0].ToString();
            //txtRegisteredDate.Text = Listview1.SelectedDataKey[0].ToString();
            //txtRegisteredBy.Text = Listview1.SelectedDataKey[0].ToString();
            //drpCreatedBy.SelectedValue = Listview1.SelectedDataKey[0].ToString();
            //txtCreatedDate.Text = Listview1.SelectedDataKey[0].ToString();

        }
        protected void btnEditLable_Click(object sender, EventArgs e)
        {
            if (Session["LANGUAGE"].ToString() == "ar-KW")
            {
                if (btnEditLable.Text == "Update Label")
                {

                    //2false
                    lblEventID2h.Visible = lblMyID2h.Visible = lblRegisteredAs2h.Visible = lblRegistrationID2h.Visible = lblPaidBy2h.Visible = lblAmountPaid2h.Visible = lblPaymentReference2h.Visible = lblNotes2h.Visible = false;//lblMyTransID2h.Visible = lblBatchPrintedBy2h.Visible = lblBatchPrintedDateTime2h.Visible = lblCertPrintedBy2h.Visible = lblCertPrintedDateTime2h.Visible = false;
                    //2true
                    txtEventID2h.Visible = txtMyID2h.Visible = txtRegisteredAs2h.Visible = txtRegistrationID2h.Visible = txtPaidBy2h.Visible = txtAmountPaid2h.Visible = txtPaymentReference2h.Visible = txtNotes2h.Visible = true;//txtMyTransID2h.Visible = txtBatchPrintedBy2h.Visible = txtBatchPrintedDateTime2h.Visible = txtCertPrintedBy2h.Visible = txtCertPrintedDateTime2h.Visible = true;

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
                    lblEventID2h.Visible = lblMyID2h.Visible = lblRegisteredAs2h.Visible = lblRegistrationID2h.Visible = lblPaidBy2h.Visible = lblAmountPaid2h.Visible = lblPaymentReference2h.Visible = lblNotes2h.Visible = true;//lblMyTransID2h.Visible = lblBatchPrintedBy2h.Visible = lblBatchPrintedDateTime2h.Visible = lblCertPrintedBy2h.Visible = lblCertPrintedDateTime2h.Visible = true;
                    //2false
                    txtEventID2h.Visible = txtMyID2h.Visible = txtRegisteredAs2h.Visible = txtRegistrationID2h.Visible = txtPaidBy2h.Visible = txtAmountPaid2h.Visible = txtPaymentReference2h.Visible = txtNotes2h.Visible = false;//txtMyTransID2h.Visible = txtBatchPrintedBy2h.Visible = txtBatchPrintedDateTime2h.Visible = txtCertPrintedBy2h.Visible = txtCertPrintedDateTime2h.Visible = false;

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
                    lblEventID1s.Visible = lblMyID1s.Visible = lblRegisteredAs1s.Visible = lblRegistrationID1s.Visible = lblPaidBy1s.Visible = lblAmountPaid1s.Visible = lblPaymentReference1s.Visible = lblNotes1s.Visible = false; //lblMyTransID1s.Visible = lblBatchPrintedBy1s.Visible = lblBatchPrintedDateTime1s.Visible = lblCertPrintedBy1s.Visible = lblCertPrintedDateTime1s.Visible =
                    //1true
                    txtEventID1s.Visible = txtMyID1s.Visible = txtRegisteredAs1s.Visible = txtRegistrationID1s.Visible = txtPaidBy1s.Visible = txtAmountPaid1s.Visible = txtPaymentReference1s.Visible = txtNotes1s.Visible = true; //txtMyTransID1s.Visible = txtBatchPrintedBy1s.Visible = txtBatchPrintedDateTime1s.Visible = txtCertPrintedBy1s.Visible = txtCertPrintedDateTime1s.Visible = 
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
                    lblEventID1s.Visible = lblMyID1s.Visible = lblRegisteredAs1s.Visible = lblRegistrationID1s.Visible = lblPaidBy1s.Visible = lblAmountPaid1s.Visible = lblPaymentReference1s.Visible = lblNotes1s.Visible = true; // lblMyTransID1s.Visible = lblBatchPrintedBy1s.Visible = lblBatchPrintedDateTime1s.Visible = lblCertPrintedBy1s.Visible = lblCertPrintedDateTime1s.Visible = 
                    //1false
                    txtEventID1s.Visible = txtMyID1s.Visible = txtRegisteredAs1s.Visible = txtRegistrationID1s.Visible = txtPaidBy1s.Visible = txtAmountPaid1s.Visible = txtPaymentReference1s.Visible = txtNotes1s.Visible = false; //txtMyTransID1s.Visible = txtBatchPrintedBy1s.Visible = txtBatchPrintedDateTime1s.Visible = txtCertPrintedBy1s.Visible = txtCertPrintedDateTime1s.Visible =
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

            List<Database.TBLLabelDTL> List = ((AcmMaster)this.Master).Bindxml("Tbl_Event_Register").Where(p => p.LabelMstID == PID && p.LANGDISP == lang).ToList();
            foreach (Database.TBLLabelDTL item in List)
            {
                if (lblEventID1s.ID == item.LabelID)
                    txtEventID1s.Text = lblEventID1s.Text = item.LabelName;
                else if (lblMyID1s.ID == item.LabelID)
                    txtMyID1s.Text = lblMyID1s.Text = item.LabelName;
                //else if (lblContactMyID1s.ID == item.LabelID)
                //    txtContactMyID1s.Text = lblContactMyID1s.Text = item.LabelName;
                else if (lblRegisteredAs1s.ID == item.LabelID)
                    txtRegisteredAs1s.Text = lblRegisteredAs1s.Text = item.LabelName;
                else if (lblRegistrationID1s.ID == item.LabelID)
                    txtRegistrationID1s.Text = lblRegistrationID1s.Text = item.LabelName;
                //else if (lblMyTransID1s.ID == item.LabelID)
                //    txtMyTransID1s.Text = lblMyTransID1s.Text = item.LabelName;
                else if (lblPaidBy1s.ID == item.LabelID)
                    txtPaidBy1s.Text = lblPaidBy1s.Text = item.LabelName;
                else if (lblAmountPaid1s.ID == item.LabelID)
                    txtAmountPaid1s.Text = lblAmountPaid1s.Text = item.LabelName;
                else if (lblPaymentReference1s.ID == item.LabelID)
                    txtPaymentReference1s.Text = lblPaymentReference1s.Text = item.LabelName;
                else if (lblNotes1s.ID == item.LabelID)
                    txtNotes1s.Text = lblNotes1s.Text = item.LabelName;
                //else if (lblBatchPrintedBy1s.ID == item.LabelID)
                //    txtBatchPrintedBy1s.Text = lblBatchPrintedBy1s.Text = lblhBatchPrintedBy.Text = item.LabelName;
                //else if (lblBatchPrintedDateTime1s.ID == item.LabelID)
                //    txtBatchPrintedDateTime1s.Text = lblBatchPrintedDateTime1s.Text = lblhBatchPrintedDateTime.Text = item.LabelName;
                //else if (lblCertPrintedBy1s.ID == item.LabelID)
                //    txtCertPrintedBy1s.Text = lblCertPrintedBy1s.Text = lblhCertPrintedBy.Text = item.LabelName;
                //else if (lblCertPrintedDateTime1s.ID == item.LabelID)
                //    txtCertPrintedDateTime1s.Text = lblCertPrintedDateTime1s.Text = lblhCertPrintedDateTime.Text = item.LabelName;

                else if (lblEventID2h.ID == item.LabelID)
                    txtEventID2h.Text = lblEventID2h.Text = item.LabelName;
                else if (lblMyID2h.ID == item.LabelID)
                    txtMyID2h.Text = lblMyID2h.Text = item.LabelName;
                //else if (lblContactMyID2h.ID == item.LabelID)
                //    txtContactMyID2h.Text = lblContactMyID2h.Text = item.LabelName;
                else if (lblRegisteredAs2h.ID == item.LabelID)
                    txtRegisteredAs2h.Text = lblRegisteredAs2h.Text = item.LabelName;
                else if (lblRegistrationID2h.ID == item.LabelID)
                    txtRegistrationID2h.Text = lblRegistrationID2h.Text = item.LabelName;
                //else if (lblMyTransID2h.ID == item.LabelID)
                //    txtMyTransID2h.Text = lblMyTransID2h.Text = item.LabelName;
                else if (lblPaidBy2h.ID == item.LabelID)
                    txtPaidBy2h.Text = lblPaidBy2h.Text = item.LabelName;
                else if (lblAmountPaid2h.ID == item.LabelID)
                    txtAmountPaid2h.Text = lblAmountPaid2h.Text = item.LabelName;
                else if (lblPaymentReference2h.ID == item.LabelID)
                    txtPaymentReference2h.Text = lblPaymentReference2h.Text = item.LabelName;
                else if (lblNotes2h.ID == item.LabelID)
                    txtNotes2h.Text = lblNotes2h.Text = item.LabelName;
                //else if (lblBatchPrintedBy2h.ID == item.LabelID)
                //    txtBatchPrintedBy2h.Text = lblBatchPrintedBy2h.Text = item.LabelName;
                //else if (lblBatchPrintedDateTime2h.ID == item.LabelID)
                //    txtBatchPrintedDateTime2h.Text = lblBatchPrintedDateTime2h.Text = item.LabelName;
                //else if (lblCertPrintedBy2h.ID == item.LabelID)
                //    txtCertPrintedBy2h.Text = lblCertPrintedBy2h.Text = item.LabelName;
                //else if (lblCertPrintedDateTime2h.ID == item.LabelID)
                //    txtCertPrintedDateTime2h.Text = lblCertPrintedDateTime2h.Text = item.LabelName;

                else
                    txtHeader.Text = lblHeader.Text = Label5.Text = item.LabelName;
            }

        }
        public void SaveLabel(string lang)
        {
            string PID = ((AcmMaster)this.Master).getOwnPage();
            //List<Database.TBLLabelDTL> List = DB.TBLLabelDTLs.Where(p => p.LabelMstID == PID  && p.LANGDISP == lang).ToList();
            List<Database.TBLLabelDTL> List = ((AcmMaster)this.Master).Bindxml("Tbl_Event_Register").Where(p => p.LabelMstID == PID && p.LANGDISP == lang).ToList();
            DataSet ds = new DataSet();
            ds.ReadXml(Server.MapPath("\\Master\\xml\\Tbl_Event_Register.xml"));
            foreach (Database.TBLLabelDTL item in List)
            {

                var obj = ((AcmMaster)this.Master).Bindxml("Tbl_Event_Register").Single(p => p.LabelID == item.LabelID && p.LabelMstID == PID && p.LANGDISP == lang);
                int i = obj.ID - 1;

                if (lblEventID1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtEventID1s.Text;
                else if (lblMyID1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtMyID1s.Text;
                //else if (lblContactMyID1s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtContactMyID1s.Text;
                else if (lblRegisteredAs1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtRegisteredAs1s.Text;
                else if (lblRegistrationID1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtRegistrationID1s.Text;
                //else if (lblMyTransID1s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtMyTransID1s.Text;
                else if (lblPaidBy1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtPaidBy1s.Text;
                else if (lblAmountPaid1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtAmountPaid1s.Text;
                else if (lblPaymentReference1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtPaymentReference1s.Text;
                else if (lblNotes1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtNotes1s.Text;
                //else if (lblBatchPrintedBy1s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtBatchPrintedBy1s.Text;
                //else if (lblBatchPrintedDateTime1s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtBatchPrintedDateTime1s.Text;
                //else if (lblCertPrintedBy1s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtCertPrintedBy1s.Text;
                //else if (lblCertPrintedDateTime1s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtCertPrintedDateTime1s.Text;

                else if (lblEventID2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtEventID2h.Text;
                else if (lblMyID2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtMyID2h.Text;
                //else if (lblContactMyID2h.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtContactMyID2h.Text;
                else if (lblRegisteredAs2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtRegisteredAs2h.Text;
                else if (lblRegistrationID2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtRegistrationID2h.Text;
                //else if (lblMyTransID2h.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtMyTransID2h.Text;
                else if (lblPaidBy2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtPaidBy2h.Text;
                else if (lblAmountPaid2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtAmountPaid2h.Text;
                else if (lblPaymentReference2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtPaymentReference2h.Text;
                else if (lblNotes2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtNotes2h.Text;
                //else if (lblBatchPrintedBy2h.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtBatchPrintedBy2h.Text;
                //else if (lblBatchPrintedDateTime2h.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtBatchPrintedDateTime2h.Text;
                //else if (lblCertPrintedBy2h.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtCertPrintedBy2h.Text;
                //else if (lblCertPrintedDateTime2h.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtCertPrintedDateTime2h.Text;


                else
                    ds.Tables[0].Rows[i]["LabelName"] = txtHeader.Text;
            }
            ds.WriteXml(Server.MapPath("\\Master\\xml\\Tbl_Event_Register.xml"));

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
            drpEventID.Enabled = false;
            drpMyID.Enabled = false;
            drpMD.Enabled = true;
            //txtContactMyID.Enabled = true;
            drpRegisteredAs.Enabled = true;
            //txtRegistrationID.Enabled = true;
            txtSearch.Enabled = true;
            txtContact.Enabled = true;
            txtPosition.Enabled = true;
            txtAttendes.Enabled = false;
            txtMobile.Enabled = true;
            txtBussinessPhone.Enabled = true;
            txtEmailId.Enabled = true;
            txtAddress.Enabled = true;
            //drpMyTransID.Enabled = true;
            drpPaydby.Enabled = true;
            txtAmountPaid.Enabled = true;
            txtPaymentReference.Enabled = true;
            //drpPaymentTermsId.Enabled = true;
            txtNotes.Enabled = true;
            lkbCustomerN1.Enabled = true;
            lkbContact.Enabled = true;
            lkbPositions.Enabled = true;
            drpCompanyName.Enabled = true;
            drpContacts.Enabled = true;
            drpPositions.Enabled = true;
            CHKListCompany.Enabled = true;
            CHKListContact.Enabled = true;
            CHKListPosition.Enabled = true;
            // txtAttendedTime.Enabled = true;
            //txtBatchPrintedBy.Enabled = true;
            //txtBatchPrintedDateTime.Enabled = true;
            //txtCertPrintedBy.Enabled = true;
            //txtCertPrintedDateTime.Enabled = true;
            //txtRegisteredDate.Enabled = true;
            //txtRegisteredBy.Enabled = true;
            //drpCreatedBy.Enabled = true;
            //txtCreatedDate.Enabled = true;

        }
        public void Readonly()
        {
            //navigation.Visible = true;
            drpEventID.Enabled = false;
            drpMyID.Enabled = false;
            drpMD.Enabled = false;
            txtSearch.Enabled = false;
            txtContact.Enabled = false;
            txtPosition.Enabled = false;
            txtAttendes.Enabled = false;
            txtMobile.Enabled = false;
            txtBussinessPhone.Enabled = false;
            txtEmailId.Enabled = false;
            txtAddress.Enabled = false;
            drpRegisteredAs.Enabled = false;
            //txtRegistrationID.Enabled = false;
            //drpMyTransID.Enabled = false;
            drpPaydby.Enabled = false;
            txtAmountPaid.Enabled = false;
            txtPaymentReference.Enabled = false;
            //drpPaymentTermsId.Enabled = false;
            txtNotes.Enabled = false;
            lkbCustomerN1.Enabled = false;
            lkbContact.Enabled = false;
            lkbPositions.Enabled = false;
            drpCompanyName.Enabled = false;
            drpContacts.Enabled = false;
            drpPositions.Enabled = false;
            CHKListCompany.Enabled = false;
            CHKListContact.Enabled = false;
            CHKListPosition.Enabled = false;
            //txtAttendedTime.Enabled = false;
            //txtBatchPrintedBy.Enabled = false;
            //txtBatchPrintedDateTime.Enabled = false;
            //txtCertPrintedBy.Enabled = false;
            //txtCertPrintedDateTime.Enabled = false;
            //txtRegisteredDate.Enabled = false;
            //txtRegisteredBy.Enabled = false;
            //drpCreatedBy.Enabled = false;
            //txtCreatedDate.Enabled = false;


        }

        #region Listview
        protected void btnNext1_Click(object sender, EventArgs e)
        {

            int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
            int Totalrec = DB.Tbl_Event_Register.Count();
            if (ViewState["Take"] == null && ViewState["Skip"] == null)
            {
                ViewState["Take"] = Showdata;
                ViewState["Skip"] = 0;
            }
            take = Convert.ToInt32(ViewState["Take"]);
            take = take + Showdata;
            Skip = take - Showdata;

            ((AcmMaster)Page.Master).BindList(Listview1, (DB.Tbl_Event_Register.Where(m => m.TenantID == TID).OrderBy(m => m.EventID).Take(take).Skip(Skip)).ToList());
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
                int Totalrec = DB.Tbl_Event_Register.Count();
                Skip = Convert.ToInt32(ViewState["Skip"]);
                take = Skip;
                Skip = take - Showdata;
                ((AcmMaster)Page.Master).BindList(Listview1, (DB.Tbl_Event_Register.Where(m => m.TenantID == TID).OrderBy(m => m.EventID).Take(take).Skip(Skip)).ToList());
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
                int Totalrec = DB.Tbl_Event_Register.Count();
                take = Showdata;
                Skip = 0;
                ((AcmMaster)Page.Master).BindList(Listview1, (DB.Tbl_Event_Register.OrderBy(m => m.EventID).Take(take).Skip(Skip)).ToList());
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
            int Totalrec = DB.Tbl_Event_Register.Count();
            take = Totalrec;
            Skip = Totalrec - Showdata;
            ((AcmMaster)Page.Master).BindList(Listview1, (DB.Tbl_Event_Register.Where(m => m.TenantID == TID).OrderBy(m => m.EventID).Take(take).Skip(Skip)).ToList());
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
                //try
                //{
                if (e.CommandName == "btnDelete")
                {

                    //string[] ID = e.CommandArgument.ToString().Split(',');
                    //string str1 = ID[0].ToString();
                    //string str2 = ID[1].ToString();

                    int id = Convert.ToInt32(e.CommandArgument);
                    Database.Tbl_Event_Register objSOJobDesc = DB.Tbl_Event_Register.Single(p => p.TenantID == id);
                    //objSOJobDesc.Active = false;
                    DB.SaveChanges();
                    Classes.Toastr.ShowToast(Page, Classes.Toastr.ToastType.Success, "Data Deleted Successfully", "Deleted!", Classes.Toastr.ToastPosition.TopCenter);
                    BindData();

                }

                if (e.CommandName == "btnEdit")
                {
                    string[] ID = e.CommandArgument.ToString().Split(',');
                    int MainEveID = Convert.ToInt32(ID[0]);
                    int myID = Convert.ToInt32(ID[1]);
                    int ContactMYID = Convert.ToInt32(ID[2]);

                    Database.Tbl_Event_Register objTbl_Event_Register = DB.Tbl_Event_Register.Single(p => p.TenantID == TID && p.EventID == MainEveID && p.MyID == myID && p.ContactMyID == ContactMYID);

                    //txtContactMyID.Text = objTbl_Event_Register.ContactMyID.ToString();
                    int AID = Convert.ToInt32(objTbl_Event_Register.ContactMyID);
                    Attendee(AID);
                    txtContact.Text = objTbl_Event_Register.Attendee.ToString();
                    txtSearch.Text = objTbl_Event_Register.Attendee.ToString();
                    int CID = Convert.ToInt32(objTbl_Event_Register.CompanyId);
                    company(CID);
                    txtSearch.Text = objTbl_Event_Register.CompanyName.ToString();
                    int PID = Convert.ToInt32(objTbl_Event_Register.positionId);
                    Position(PID);
                    txtPosition.Text = objTbl_Event_Register.PositionName.ToString();
                    txtAttendes.Text = objTbl_Event_Register.Attendee.ToString();
                    //updateAttendee.Update();
                    txtMobile.Text = objTbl_Event_Register.MobileNo.ToString();
                    txtBussinessPhone.Text = objTbl_Event_Register.Busphone.ToString();
                    txtEmailId.Text = objTbl_Event_Register.Email.ToString();
                    txtAddress.Text = objTbl_Event_Register.Address.ToString();
                    txtNotes.Text = objTbl_Event_Register.Notes.ToString();
                    drpRegisteredAs.SelectedValue = objTbl_Event_Register.RegisteredAs.ToString();
                    drpPaydby.SelectedValue = objTbl_Event_Register.PaidBy.ToString();
                    txtAmountPaid.Text = Convert.ToDecimal(objTbl_Event_Register.AmountPaid).ToString();
                    txtPaymentReference.Text = objTbl_Event_Register.PaymentReference.ToString();
                    txtRegistrationID.Text = objTbl_Event_Register.RegistrationID.ToString();
                    //UpdateRegistration.Update();

                    Avatar.ImageUrl = "~/Master/Img/" + objTbl_Event_Register.IMG;

                    lblwalkondate.Text = "Walk-In on Date / Time MM/dd/yyyy 00:00 AM" + "(" + objTbl_Event_Register.RegisteredDate.ToString() + ").";
                    lblBatchPrintedon.Text = "Batch Printed on Date / Time MM/dd/yyyy 00:00 AM " + "(" + objTbl_Event_Register.BatchPrintedDateTime.ToString() + ")" + " Batch Printed By " + "(" + objTbl_Event_Register.BatchPrintedBy + ").";
                    lblCertificatePrintedon.Text = "Certificate Printed on Date / Time MM/dd/yyyy 00:00 AM " + "(" + objTbl_Event_Register.CertPrintedDateTime.ToString() + ")" + " Certificate Printed By " + "(" + objTbl_Event_Register.CertPrintedBy + ").";
                    pnlviewonly.Visible = true;
                    btnAdd.Text = "Update";
                    if (objTbl_Event_Register.PaymentTermsId != null)
                        btnsaveprint.Text = "Update & Print";
                    btnsaveContinue.Text = "Update & Continue";
                    ViewState["Edit"] = ID;
                    ViewState["MainEveID"] = MainEveID;
                    ViewState["myID"] = myID;
                    ViewState["ContactMYID"] = ContactMYID;
                    ViewState["Contact"] = ContactMYID;
                    ViewState["COM"] = MainEveID;
                    ViewState["CON"] = myID;
                    FillContractorID();
                    Write();
                    DrpRead();
                    string barcode = objTbl_Event_Register.BARCODE.ToString();
                    showBarcode(barcode);
                }
                scope.Complete(); //  To commit.
            }
            //    catch (Exception ex)
            //    {
            //        ScriptManager.RegisterClientScriptBlock(sender as Control, this.GetType(), "alert", ex.Message, true);
            //        throw;
            //    }
            //}
        }
        protected void ListView2_ItemCommand(object sender, ListViewCommandEventArgs e)
        {

            int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
            int Totalrec = DB.Tbl_Event_Register.Count();
            if (e.CommandName == "LinkPageavigation")
            {
                int ID = Convert.ToInt32(e.CommandArgument);
                ViewState["Take"] = ID * Showdata;
                ViewState["Skip"] = (ID * Showdata) - Showdata;
                int Tvalue = Convert.ToInt32(ViewState["Take"]);
                int Svalue = Convert.ToInt32(ViewState["Skip"]);
                ((AcmMaster)Page.Master).BindList(Listview1, (DB.Tbl_Event_Register.Where(m => m.TenantID == TID).OrderBy(m => m.ContactMyID).Take(Tvalue).Skip(Svalue)).ToList());
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
        protected void lkbCustomerN1_Click(object sender, EventArgs e)
        {
            string companysearch = txtSearch.Text.ToString();
            List<Database.TBLCOMPANYSETUP> list1 = DB.TBLCOMPANYSETUPs.Where(p => (p.COMPNAME1.ToUpper().Contains(companysearch.ToUpper()) || p.MOBPHONE.Contains(companysearch) || p.BUSPHONE1.Contains(companysearch) || p.ADDR1.ToUpper().Contains(companysearch.ToUpper()) || p.EMAIL.ToUpper().Contains(companysearch.ToUpper()) || p.BARCODE.Contains(companysearch)) && p.TenentID == TID).ToList();

            if (list1.Count() == 0)
            {
                list1 = DB.TBLCOMPANYSETUPs.Where(p => (p.COMPNAME1.ToUpper().Contains(companysearch.ToUpper()) || p.MOBPHONE.Contains(companysearch) || p.BUSPHONE1.Contains(companysearch) || p.ADDR1.ToUpper().Contains(companysearch.ToUpper()) || p.EMAIL.ToUpper().Contains(companysearch.ToUpper()) || p.BARCODE.Contains(companysearch))).ToList();
                var ComListAll = from Itemall in list1
                                 select new
                                 {
                                     Name = Itemall.COMPNAME1 + " - " + ((Int32)Itemall.TenentID).ToString(),
                                     IDd = Itemall.COMPID
                                 };
                drpCompanyName.DataSource = ComListAll.OrderBy(p => p.IDd);
                drpCompanyName.DataTextField = "Name";
                drpCompanyName.DataValueField = "IDd";
                drpCompanyName.DataBind();
                drpCompanyName.Items.Insert(0, new ListItem("-- Select Company--", "0"));
            }
            else
            {
                var ComListAll = from Itemall in list1
                                 select new
                                 {
                                     Name = Itemall.COMPNAME1 + " - " + ((Int32)Itemall.TenentID).ToString(),
                                     IDd = Itemall.COMPID
                                 };
                drpCompanyName.DataSource = ComListAll.OrderBy(p => p.IDd);
                drpCompanyName.DataTextField = "Name";
                drpCompanyName.DataValueField = "IDd";
                drpCompanyName.DataBind();
                drpCompanyName.Items.Insert(0, new ListItem("-- Select Company--", "0"));
            }

            int Count = list1.Count();
            if (Count == 1)
            {
                drpCompanyName.SelectedIndex = 1;
                lblCompanyNames.Visible = true;                
                lblCompanyNames.Text = Count + " Companies Found";
                CHKListCompany.Checked = false;
            }
            else if (Count > 1)
            {
                lblCompanyNames.Visible = true;
                lblCompanyNames.Text = Count + " Companies Found Use DropDown to Select";
                CHKListCompany.Checked = false;
            }
            else
            {
                //lblCompanyNames.Visible = true;
                //lblCompanyNames.Text = " No Records Founds";
                CHKListCompany.Checked = true;
                //pnlComSearch.Visible = false;
                //lkbCustomerN1.Visible = false;
                lblCompanyNames.Visible = true;
                lblCompanyNames.Text = "If You Have Not Any Comapany, Check a checkbox & Type Your Company Name In Company Search";
            }
        }
        protected void drpCompanyName_SelectedIndexChanged(object sender, EventArgs e)
        {
            int CompID = Convert.ToInt32(drpCompanyName.SelectedValue);
            if (CompID != 0)
            {
                if (DB.TBLCOMPANYSETUPs.Where(p => p.TenentID == TID && p.COMPID == CompID).Count() > 0)
                {
                    Database.TBLCOMPANYSETUP obj = DB.TBLCOMPANYSETUPs.Single(p => p.TenentID == TID && p.COMPID == CompID);
                    string company = drpCompanyName.SelectedItem.ToString();
                    txtSearch.Text = company;

                    txtMobile.Text = obj.MOBPHONE.ToString();
                    txtBussinessPhone.Text = obj.BUSPHONE1.ToString();
                    txtAddress.Text = obj.ADDR1.ToString();
                    txtEmailId.Text = obj.EMAIL.ToString();
                }
            }
        }
        protected void lkbContact_Click(object sender, EventArgs e)
        {
            if (drpCompanyName.SelectedValue == "0")
            {
                if (CHKListCompany.Checked == false)
                {
                    lblContactsa.Text = " Select Company First...";
                }
            }
            ViewState["ContactAll"] = null;
            string contactsearch = txtContact.Text.ToString();
            List<Database.TBLCONTACT> list2 = DB.TBLCONTACTs.Where(p => (p.PersName1.ToUpper().Contains(contactsearch.ToUpper()) || p.PersName2.ToUpper().Contains(contactsearch.ToUpper()) || p.PersName3.ToUpper().Contains(contactsearch.ToUpper()) || p.EMAIL1.ToUpper().Contains(contactsearch.ToUpper()) || p.MOBPHONE.Contains(contactsearch) || p.BUSPHONE1.Contains(contactsearch)) && p.TenentID == TID).ToList();// && p.Active == "Y"

            if (list2.Count() == 0)
            {
                list2 = DB.TBLCONTACTs.Where(p => (p.PersName1.ToUpper().Contains(contactsearch.ToUpper()) || p.PersName2.ToUpper().Contains(contactsearch.ToUpper()) || p.PersName3.ToUpper().Contains(contactsearch.ToUpper()) || p.EMAIL1.ToUpper().Contains(contactsearch.ToUpper()) || p.MOBPHONE.Contains(contactsearch) || p.BUSPHONE1.Contains(contactsearch))).ToList();// && p.Active == "Y"
                var ListAll = from Itemall in list2
                              select new
                              {
                                  Name = Itemall.PersName1 + " - " + ((Int32)Itemall.TenentID).ToString(),
                                  IDd = Itemall.ContactMyID
                              };
                drpContacts.DataSource = ListAll.OrderBy(p => p.IDd);
                drpContacts.DataTextField = "Name";
                drpContacts.DataValueField = "IDd";
                drpContacts.DataBind();
                drpContacts.Items.Insert(0, new ListItem("-- Select Attendee--", "0"));


            }
            else
            {
                var ListAll = from Itemall in list2
                              select new
                              {
                                  Name = Itemall.PersName1 + " - " + ((Int32)Itemall.TenentID).ToString(),
                                  IDd = Itemall.ContactMyID
                              };
                drpContacts.DataSource = ListAll.OrderBy(p => p.IDd);
                drpContacts.DataTextField = "Name";
                drpContacts.DataValueField = "IDd";
                drpContacts.DataBind();
                drpContacts.Items.Insert(0, new ListItem("-- Select Attendee--", "0"));
            }
            int Count = list2.Count();
            if (Count == 1)
            {
                drpContacts.SelectedIndex = 1;
                lblContactsa.Visible = true;
                lblContactsa.Text = Count + " Contact found";
                FillAttendee();
                CHKListContact.Checked = false;
            }
            else if (Count > 1)
            {
                lblContactsa.Visible = true;
                lblContactsa.Text = Count + " Contact found Use DropDown to Select";
                CHKListContact.Checked = false;
            }
            else
            {
                CHKListContact.Checked = true;
                //lkbContact.Visible = false;
                //pnlContSearch.Visible = false;
                lblContactsa.Visible = true;
                lblContactsa.Text = "If You Have Not Any Contact, Check a checkbox & Type Your Contact Name In Contact Search";
                registrationID();
            }
        }
        public int Contacall(int Tenent, int CID)
        {

            int CompanyID = Convert.ToInt32(drpCompanyName.SelectedValue);
            Database.TBLCONTACT obj_Contact = DB.TBLCONTACTs.Single(p => p.TenentID == Tenent && p.ContactMyID == CID);
            Database.TBLCONTACT obj_Compay = new Database.TBLCONTACT();
            obj_Compay.TenentID = TID;
            int Conract = DB.TBLCONTACTs.Where(p => p.TenentID == TID).Count() > 0 ? Convert.ToInt32(DB.TBLCONTACTs.Where(p => p.TenentID == TID).Max(p => p.ContactMyID) + 1) : 1;
            obj_Compay.ContactMyID = Conract;
            obj_Compay.CONTACTID = Conract;
            obj_Compay.PersName1 = obj_Contact.PersName1;
            obj_Compay.PersName2 = obj_Contact.PersName2;
            obj_Compay.PersName3 = obj_Contact.PersName3;
            obj_Compay.PHYSICALLOCID = "WKT";
            obj_Compay.COUNTRYID = 126;
            obj_Compay.EMAIL1 = obj_Contact.EMAIL1;
            obj_Compay.ADDR1 = obj_Contact.ADDR1;
            obj_Compay.BUSPHONE1 = obj_Contact.BUSPHONE1;
            obj_Compay.MOBPHONE = obj_Contact.MOBPHONE;
            obj_Compay.Active = "Y";
            obj_Compay.BUYER = true;
            obj_Compay.BARCODE = TID.ToString() + Conract.ToString();
            obj_Compay.COMPANYID = CompanyID;
            DB.TBLCONTACTs.AddObject(obj_Compay);
            DB.SaveChanges();
            return Conract;
            //ViewState["ContactAll"] = contactallTemp;
        }
        public int companyall(int Tenent, int Comp)
        {
            Database.TBLCOMPANYSETUP Tempobj = DB.TBLCOMPANYSETUPs.Single(p => p.TenentID == Tenent && p.COMPID == Comp);
            Database.TBLCOMPANYSETUP obj_Compay = new Database.TBLCOMPANYSETUP();
            obj_Compay.TenentID = TID;
            int ID = DB.TBLCOMPANYSETUPs.Where(p => p.TenentID == TID).Count() > 0 ? Convert.ToInt32(DB.TBLCOMPANYSETUPs.Where(p => p.TenentID == TID).Max(p => p.COMPID) + 1) : 1;
            obj_Compay.COMPID = ID;
            obj_Compay.COMPNAME1 = Tempobj.COMPNAME1;
            obj_Compay.COMPNAME2 = Tempobj.COMPNAME2;
            obj_Compay.COMPNAME3 = Tempobj.COMPNAME3;
            obj_Compay.COUNTRYID = 126;
            obj_Compay.PHYSICALLOCID = "WKT";
            obj_Compay.EMAIL = Tempobj.EMAIL;
            obj_Compay.EMAIL1 = Tempobj.EMAIL1;
            obj_Compay.EMAIL2 = Tempobj.EMAIL2;
            obj_Compay.ADDR1 = Tempobj.ADDR1;
            obj_Compay.ADDR2 = Tempobj.ADDR2;
            obj_Compay.STATE = "1902";
            obj_Compay.FAX = Tempobj.FAX;
            obj_Compay.FAX1 = Tempobj.FAX1;
            obj_Compay.FAX2 = Tempobj.FAX2;
            obj_Compay.BUSPHONE1 = Tempobj.BUSPHONE1;
            obj_Compay.BUSPHONE2 = Tempobj.BUSPHONE2;
            obj_Compay.BUSPHONE3 = Tempobj.BUSPHONE3;
            obj_Compay.BUSPHONE4 = Tempobj.BUSPHONE4;
            obj_Compay.MOBPHONE = Tempobj.MOBPHONE;
            obj_Compay.ZIPCODE = Tempobj.ZIPCODE;
            obj_Compay.POSTALCODE = Tempobj.POSTALCODE;
            obj_Compay.CITY = "1";
            obj_Compay.CompanyType = "82002";
            obj_Compay.Approved = 1;
            obj_Compay.SALER = true;
            obj_Compay.Active = "Y";
            DB.TBLCOMPANYSETUPs.AddObject(obj_Compay);
            DB.SaveChanges();
            return ID;
        }
        protected void lkbPositions_Click(object sender, EventArgs e)
        {
            string Positionssearch = txtPosition.Text.ToString();
            List<Database.Tbl_Position_Mst> list3 = DB.Tbl_Position_Mst.Where(p => (p.PositionName.ToUpper().Contains(Positionssearch.ToUpper()) || p.PositionNameAR.ToUpper().Contains(Positionssearch.ToUpper()) || p.PositionNameFR.ToUpper().Contains(Positionssearch.ToUpper())) && p.Active == true && p.TenentID == TID).OrderBy(m => m.PositionName).ToList();

            drpPositions.DataSource = list3;
            drpPositions.DataTextField = "PositionName";
            drpPositions.DataValueField = "PositionID";
            drpPositions.DataBind();
            drpPositions.Items.Insert(0, new ListItem("-- Select Company--", "0"));

            int Count = list3.Count();
            if (Count == 1)
            {
                drpPositions.SelectedIndex = 1;
                lblPositionSa.Visible = true;
                lblPositionSa.Text = Count + " Position Found";
                CHKListPosition.Checked = false;
            }
            else if (Count > 1)
            {
                lblPositionSa.Visible = true;
                lblPositionSa.Text = Count + " Position Found Use DropDown to Select";
                CHKListPosition.Checked = false;
            }
            else
            {
                CHKListPosition.Checked = true;
                //lkbPositions.Visible = false;
                //pnlPosiSearch.Visible = false;
                lblPositionSa.Visible = true;
                lblPositionSa.Text = "If You Have Not Any Position, Check a checkbox & Type Your Position In Contact Search";
            }
        }
        protected void drpPositions_SelectedIndexChanged(object sender, EventArgs e)
        {
            string Posision = drpPositions.SelectedItem.ToString();
            txtPosition.Text = Posision;
        }
        public void Position(int PID)
        {
            drpPositions.DataSource = DB.Tbl_Position_Mst.Where(p => p.Active == true && p.TenentID == TID && p.PositionID == PID);
            drpPositions.DataTextField = "PositionName";
            drpPositions.DataValueField = "PositionID";
            drpPositions.DataBind();
        }
        public void company(int CID)
        {
            drpCompanyName.DataSource = DB.TBLCOMPANYSETUPs.Where(p => p.TenentID == TID && p.COMPID == CID);
            drpCompanyName.DataTextField = "COMPNAME1";
            drpCompanyName.DataValueField = "COMPID";
            drpCompanyName.DataBind();
        }
        public void Attendee(int AID)
        {
            drpContacts.DataSource = DB.TBLCONTACTs.Where(p => p.TenentID == TID && p.ContactMyID == AID);
            drpContacts.DataTextField = "PersName1";
            drpContacts.DataValueField = "ContactMyID";
            drpContacts.DataBind();
        }
        public void DrpRead()
        {
            drpEventID.Enabled = false;
            drpMyID.Enabled = false;
            drpMD.Enabled = false;
            //txtContactMyID.Enabled = false;
        }

        protected void btnPosition_Click(object sender, EventArgs e)
        {
            lblerrorMSGPosition.Visible = false;
            if (txtAddPosition.Text != "")
            {
                if (DB.Tbl_Position_Mst.Where(p => p.PositionName.ToLower() == txtAddPosition.Text.ToLower() && p.TenentID == TID).Count() < 1)
                {
                    Database.Tbl_Position_Mst OBJPOSITION = new Database.Tbl_Position_Mst();
                    OBJPOSITION.TenentID = TID;
                    OBJPOSITION.PositionID = DB.Tbl_Position_Mst.Where(p => p.TenentID == TID && p.PositionID != 99999).Count() > 0 ? Convert.ToInt32(DB.Tbl_Position_Mst.Where(p => p.TenentID == TID && p.PositionID != 99999).Max(p => p.PositionID) + 1) : 1;
                    OBJPOSITION.PositionName = txtAddPosition.Text;
                    OBJPOSITION.PositionNameAR = Translate(txtAddPosition.Text, "ar");
                    OBJPOSITION.PositionNameFR = Translate(txtAddPosition.Text, "fr");
                    OBJPOSITION.Active = true;
                    OBJPOSITION.Datetime = DateTime.Now;
                    DB.Tbl_Position_Mst.AddObject(OBJPOSITION);
                    DB.SaveChanges();
                }
                else
                {
                    lblerrorMSGPosition.Visible = true;
                    lblerrorMSGPosition.Text = "'Position Is All Ready Exist...'";
                }
            }
            else
            {
                lblerrorMSGPosition.Visible = true;
                lblerrorMSGPosition.Text = "'Please Insert Position...'";
            }
        }

        protected void btnContact_Click(object sender, EventArgs e)
        {
            lblErrorMSGContact.Visible = false;

            if (txtAddContact.Text != "" && txtcEmail.Text != "" && txtcMobile.Text != "" && txtcBuscontact.Text != "" && txtAdd.Text != "")
            {
                if (DB.TBLCONTACTs.Where(p => p.TenentID == TID && p.PersName1.ToUpper() == txtAddContact.Text.ToUpper()).Count() < 1)
                {
                    if (drpCompanyName.SelectedValue == "" || drpCompanyName.SelectedValue == "0")
                    {
                        lblErrorMSGContact.Visible = true;
                        lblErrorMSGContact.Text = "'Please Search company name and Select company Name First...'";
                    }
                    else
                    {
                        int CompanyID = Convert.ToInt32(drpCompanyName.SelectedValue);
                        Database.TBLCONTACT obj_Compay = new Database.TBLCONTACT();
                        obj_Compay.TenentID = TID;
                        int Conract = DB.TBLCONTACTs.Where(p => p.TenentID == TID).Count() > 0 ? Convert.ToInt32(DB.TBLCONTACTs.Where(p => p.TenentID == TID).Max(p => p.ContactMyID) + 1) : 1;
                        obj_Compay.ContactMyID = Conract;
                        obj_Compay.CONTACTID = Conract;
                        obj_Compay.PersName1 = txtAddContact.Text;
                        obj_Compay.PersName2 = txtAddContact.Text;
                        obj_Compay.PersName3 = txtAddContact.Text;
                        obj_Compay.PHYSICALLOCID = "WKT";
                        obj_Compay.COUNTRYID = 126;
                        obj_Compay.EMAIL1 = txtcEmail.Text;
                        obj_Compay.ADDR1 = txtAdd.Text;
                        obj_Compay.BUSPHONE1 = txtcBuscontact.Text;
                        obj_Compay.MOBPHONE = txtcMobile.Text;
                        obj_Compay.Active = "Y";
                        obj_Compay.BUYER = true;
                        obj_Compay.BARCODE = TID.ToString() + Conract.ToString();
                        obj_Compay.COMPANYID = CompanyID;
                        DB.TBLCONTACTs.AddObject(obj_Compay);
                        DB.SaveChanges();
                    }
                }
                else
                {
                    lblErrorMSGContact.Visible = true;
                    lblErrorMSGContact.Text = "'Contact Is All Ready Exist...'";
                }
            }
            else
            {
                lblErrorMSGContact.Visible = true;
                lblErrorMSGContact.Text = "'Please Insert Contact all Information...'";
            }
        }

        protected void btnCompany_Click(object sender, EventArgs e)
        {
            lblErrorMSGCompany.Visible = false;
            if (txtaddCompany.Text != "")
            {
                if (DB.TBLCOMPANYSETUPs.Where(p => p.TenentID == TID && p.COMPNAME1.ToUpper() == txtaddCompany.Text.ToUpper()).Count() < 1)
                {
                    Database.TBLCOMPANYSETUP obj_Compay = new Database.TBLCOMPANYSETUP();
                    obj_Compay.TenentID = TID;
                    obj_Compay.COMPID = DB.TBLCOMPANYSETUPs.Where(p => p.TenentID == TID).Count() > 0 ? Convert.ToInt32(DB.TBLCOMPANYSETUPs.Where(p => p.TenentID == TID).Max(p => p.COMPID) + 1) : 1;
                    obj_Compay.COMPNAME1 = txtaddCompany.Text;
                    obj_Compay.COMPNAME2 = txtaddCompany.Text;
                    obj_Compay.COMPNAME3 = txtaddCompany.Text;
                    obj_Compay.COUNTRYID = 126;
                    obj_Compay.PHYSICALLOCID = "WKT";
                    obj_Compay.EMAIL = "NotUsed@gmail.com";
                    obj_Compay.EMAIL1 = "NotUsed@gmail.com";
                    obj_Compay.EMAIL2 = "NotUsed@gmail.com";
                    obj_Compay.ADDR1 = "Not Used";
                    obj_Compay.ADDR2 = "Not Used";
                    obj_Compay.STATE = "1902";
                    obj_Compay.FAX = "00000";
                    obj_Compay.FAX1 = "00000";
                    obj_Compay.FAX2 = "00000";
                    obj_Compay.BUSPHONE1 = "00000";
                    obj_Compay.BUSPHONE2 = "00000";
                    obj_Compay.BUSPHONE3 = "00000";
                    obj_Compay.BUSPHONE4 = "00000";
                    obj_Compay.MOBPHONE = "00000";
                    obj_Compay.ZIPCODE = "00000";
                    obj_Compay.POSTALCODE = "00000";
                    obj_Compay.CITY = "1";
                    obj_Compay.CompanyType = "82005";
                    obj_Compay.Approved = 1;
                    obj_Compay.SALER = true;
                    obj_Compay.BUYER = true;
                    obj_Compay.Active = "Y";
                    DB.TBLCOMPANYSETUPs.AddObject(obj_Compay);
                    DB.SaveChanges();
                }
                else
                {
                    lblErrorMSGCompany.Visible = false;
                    lblErrorMSGCompany.Text = "'Company Is All Ready Exist...'";
                }
            }
            else
            {
                lblErrorMSGCompany.Visible = false;
                lblErrorMSGCompany.Text = "'Please Insert Company Name...'";
            }
        }

        protected void CHKListCompany_CheckedChanged(object sender, EventArgs e)
        {
            if (CHKListCompany.Checked == true)
            {
                //pnlComSearch.Visible = false;
                //lkbCustomerN1.Visible = false;
                lblCompanyNames.Visible = true;
                lblCompanyNames.Text = "If You Have Not Any Comapany, Check a checkbox & Type Your Company Name In Company Search";
            }
            else
            {
                //pnlComSearch.Visible = true;
                //lkbCustomerN1.Visible = true;
                lblCompanyNames.Visible = false;
            }

        }

        protected void CHKListContact_CheckedChanged(object sender, EventArgs e)
        {
            if (CHKListContact.Checked == true)
            {
                //lkbContact.Visible = false;
                //pnlContSearch.Visible = false;
                lblContactsa.Visible = true;
                lblContactsa.Text = "If You Have Not Any Contact, Check a checkbox & Type Your Contact Name In Contact Search";

                registrationID();
            }
            else
            {
                //lkbContact.Visible = true;
                //pnlContSearch.Visible = true;
                lblContactsa.Visible = false;
            }
        }

        protected void CHKListPosition_CheckedChanged(object sender, EventArgs e)
        {
            if (CHKListPosition.Checked == true)
            {
                //lkbPositions.Visible = false;
                //pnlPosiSearch.Visible = false;
                lblPositionSa.Visible = true;
                lblPositionSa.Text = "If You Have Not Any Position, Check a checkbox & Type Your Position In Contact Search";
            }
            else
            {
                //lkbPositions.Visible = true;
                //pnlPosiSearch.Visible = true;
                lblPositionSa.Visible = false;
            }
        }
        public string getEvent(int EventID)
        {
            if (DB.Tbl_Event_Main.Where(p => p.TenantID == TID && p.EventID == EventID).Count() > 0)
            {
                string eventName = DB.Tbl_Event_Main.Single(p => p.TenantID == TID && p.EventID == EventID).Title2Print;
                return EventID.ToString() + " - " + eventName;
            }
            else
            {
                return "Not Found";
            }
        }
        public string getSubEventID(int EventID, int subID)
        {
            if (DB.tbl_Event_Detail.Where(p => p.TenantID == TID && p.EventID == EventID && p.MyID == subID).Count() > 0)
            {
                string SubID = DB.tbl_Event_Detail.Single(p => p.TenantID == TID && p.EventID == EventID && p.MyID == subID).Description1;
                return subID.ToString() + " - " + SubID;
            }
            else
            {
                return "Not Found";
            }
        }
        public string getPaidByID(int Paid)
        {
            if (DB.REFTABLEs.Where(p => p.TenentID == TID && p.REFID == Paid && p.SHORTNAME == "Event" && p.REFSUBTYPE == "Method" && p.REFTYPE == "Payment").Count() > 0)
            {
                string PaidBY = DB.REFTABLEs.Single(p => p.TenentID == TID && p.REFID == Paid && p.SHORTNAME == "Event" && p.REFSUBTYPE == "Method" && p.REFTYPE == "Payment").REFNAME1;
                return PaidBY;
            }
            else
            {
                return "Not Found";
            }

        }
        public string RegistrationAS(int RegID)
        {
            if (DB.REFTABLEs.Where(p => p.TenentID == TID && p.REFTYPE == "EVN" && p.REFSUBTYPE == "RegisterType" && p.REFID == RegID).Count() > 0)
            {
                string RegName = DB.REFTABLEs.Single(p => p.TenentID == TID && p.REFTYPE == "EVN" && p.REFSUBTYPE == "RegisterType" && p.REFID == RegID).REFNAME1;
                return RegName;
            }
            else
            {
                return "Not Found";
            }
        }
        public void registrationID()
        {
            int MainEventID = Convert.ToInt32(drpEventID.SelectedValue);
            int SubEventID = Convert.ToInt32(drpMyID.SelectedValue);
            Database.tbl_Event_Detail OBJDetail = DB.tbl_Event_Detail.Single(p => p.TenantID == TID && p.EventID == MainEventID && p.MyID == SubEventID);
            string regID = OBJDetail.RegisterationIDBegin.ToString();
            string regNO = OBJDetail.RegisterationNumber.ToString();
            string RegConcate = regID + "" + regNO;
            txtRegistrationID.Text = RegConcate;
            //UpdateRegistration.Update();
        }

        protected void txtAmountPaid_TextChanged(object sender, EventArgs e)
        {
            decimal Amount = Convert.ToDecimal(txtAmountPaid.Text);
            int MainEventID = Convert.ToInt32(drpEventID.SelectedValue);
            int SubEventID = Convert.ToInt32(drpMyID.SelectedValue);
            Database.tbl_Event_Detail OBJDetail = DB.tbl_Event_Detail.Single(p => p.TenantID == TID && p.EventID == MainEventID && p.MyID == SubEventID);
            if (Amount > OBJDetail.Rate)
            {
                txtAmountPaid.ForeColor = System.Drawing.ColorTranslator.FromHtml("#f44242");
            }
            else
            {
                txtAmountPaid.ForeColor = System.Drawing.ColorTranslator.FromHtml("#111111");
            }
        }

        protected void btnBarcodePrint_Click(object sender, EventArgs e)
        {

            List<Database.Tbl_Event_Register> List1 = new List<Database.Tbl_Event_Register>();
            for (int i = 0; i < Listview1.Items.Count; i++)
            {
                CheckBox CHKBarcodePrint = (CheckBox)Listview1.Items[i].FindControl("CHKBarcodePrint");
                Label LBLBARCODEE = (Label)Listview1.Items[i].FindControl("LBLBARCODEE");
                Label lblEventID = (Label)Listview1.Items[i].FindControl("lblEventID");
                Label lblMyID = (Label)Listview1.Items[i].FindControl("lblMyID");
                Label lblAttendee1 = (Label)Listview1.Items[i].FindControl("lblAttendee1");
                Label lblcompany1 = (Label)Listview1.Items[i].FindControl("lblcompany1");
                Label lblRegas1 = (Label)Listview1.Items[i].FindControl("lblRegas1");
                if (CHKBarcodePrint.Checked == true)
                {
                    int barcode = Convert.ToInt32(LBLBARCODEE.Text);
                    string[] EID = lblEventID.Text.ToString().Split('-');
                    string EventName = EID[1].ToString().Trim();
                    string[] DID = lblMyID.Text.ToString().Split('-');
                    string DetailName = DID[1].ToString().Trim();
                    string Attendee = lblAttendee1.Text;
                    string Company = lblcompany1.Text;
                    string RegitedAS = lblRegas1.Text;
                    Database.Tbl_Event_Register obj = new Database.Tbl_Event_Register();
                    obj.BARCODE = barcode.ToString();
                    obj.Address = EventName.ToString();
                    obj.Notes = DetailName.ToString();
                    obj.Attendee = Attendee.ToString();
                    obj.CompanyName = Company.ToString();
                    obj.PaymentReference = RegitedAS.ToString();
                    List1.Add(obj);
                }
            }
            Session["Barcode"] = List1;
            Response.Redirect("EventBarcode.aspx");
        }





        protected void CHKBarcodeAll_CheckedChanged(object sender, EventArgs e)
        {

            for (int i = 0; i < Listview1.Items.Count; i++)
            {
                CheckBox CHKBarcodePrint = (CheckBox)Listview1.Items[i].FindControl("CHKBarcodePrint");
                if (CHKBarcodeAll.Checked == true)
                {
                    CHKBarcodePrint.Checked = true;
                }
                else
                {
                    CHKBarcodePrint.Checked = false;
                }
            }
        }
        public void saveandcontinue()
        {
            List<Database.Tbl_Event_Register> List = DB.Tbl_Event_Register.Where(p => p.TenantID == TID).ToList();
            Database.Tbl_Event_Register objTbl_Event_Register = new Database.Tbl_Event_Register();
            //Server Content Send data Yogesh
            objTbl_Event_Register.TenantID = TID;
            int MainEventID = Convert.ToInt32(drpEventID.SelectedValue);
            int SubEventID = Convert.ToInt32(drpMyID.SelectedValue);
            int contactmyid = Convert.ToInt32(drpContacts.SelectedValue);
            if (List.Where(p => p.EventID == MainEventID && p.MyID == SubEventID && p.ContactMyID == contactmyid).Count() > 0)
            {
                pnldanger.Visible = true;
                lbldmsg.Text = "This Attendee Registration Is AllReady Exist...";
                //return;
            }
            else
            {
                objTbl_Event_Register.EventID = MainEventID;
                objTbl_Event_Register.MyID = SubEventID;

                //objTbl_Event_Register.ContactMyID = DB.Tbl_Event_Register.Where(p=>p.TenantID == TID && p.EventID == MainEventID && p.MyID == SubMyID).Count()>0? Convert.ToInt32(DB.Tbl_Event_Register.Where(p=>p.TenantID == TID && p.EventID == MainEventID && p.MyID == SubMyID).Max(p=>p.ContactMyID)+1):1; //Convert.ToInt32(txtContactMyID.Text);
                objTbl_Event_Register.RegisteredAs = Convert.ToInt32(drpRegisteredAs.SelectedValue);
                objTbl_Event_Register.RegistrationID = txtRegistrationID.Text;
                //objTbl_Event_Register.MyTransID = Convert.ToInt32(drpMyTransID.SelectedValue);

                if (CHKListCompany.Checked == true)
                    objTbl_Event_Register.CompanyId = 20;
                else
                    objTbl_Event_Register.CompanyId = Convert.ToInt32(drpCompanyName.SelectedValue);

                if (CHKListCompany.Checked == true)
                    objTbl_Event_Register.CompanyName = txtSearch.Text;
                else
                    objTbl_Event_Register.CompanyName = drpCompanyName.SelectedItem.ToString();

                if (CHKListCompany.Checked == true)
                    objTbl_Event_Register.ContactMyID = DB.Tbl_Event_Register.Where(p => p.TenantID == TID && p.EventID == MainEventID && p.MyID == SubEventID && p.ContactMyID >= 99999).Count() > 0 ? Convert.ToInt32(DB.Tbl_Event_Register.Where(p => p.TenantID == TID && p.EventID == MainEventID && p.MyID == SubEventID && p.ContactMyID >= 99999).Max(p => p.ContactMyID) + 1) : 99999;
                else
                    objTbl_Event_Register.ContactMyID = Convert.ToInt32(drpContacts.SelectedValue);

                if (CHKListCompany.Checked == true)
                    objTbl_Event_Register.Attendee = txtContact.Text;
                else
                    objTbl_Event_Register.Attendee = txtAttendes.Text;//drpContacts.SelectedItem.ToString();

                if (CHKListCompany.Checked == true)
                    objTbl_Event_Register.PositionName = txtPosition.Text;
                else
                    objTbl_Event_Register.PositionName = drpPositions.SelectedItem.ToString();

                if (CHKListCompany.Checked == true)
                    objTbl_Event_Register.positionId = 000;
                else
                    objTbl_Event_Register.positionId = Convert.ToInt32(drpPositions.SelectedValue);

                if (FileUpload1.HasFile)
                {
                    string path = FileUpload1.FileName;
                    FileUpload1.SaveAs(Server.MapPath("~/Master/Img/" + path));
                    objTbl_Event_Register.IMG = path;
                }

                objTbl_Event_Register.MobileNo = txtMobile.Text;
                objTbl_Event_Register.Busphone = txtBussinessPhone.Text;
                objTbl_Event_Register.Email = txtEmailId.Text;
                objTbl_Event_Register.Address = txtAddress.Text;
                if (Convert.ToInt32(drpPaydby.SelectedValue) != 0)
                    objTbl_Event_Register.PaidBy = Convert.ToInt32(drpPaydby.SelectedValue);
                else
                    objTbl_Event_Register.PaidBy = 2051;
                objTbl_Event_Register.AmountPaid = Convert.ToDecimal(txtAmountPaid.Text);
                objTbl_Event_Register.PaymentReference = txtPaymentReference.Text;
                objTbl_Event_Register.Notes = txtNotes.Text;
                //objTbl_Event_Register.PaymentTermsId = Convert.ToInt32(drpPaymentTermsId.SelectedValue);                                
                //objTbl_Event_Register.AttendedTime = Convert.ToDateTime(txtAttendedTime.Text);
                objTbl_Event_Register.BatchPrintedBy = UserName;
                objTbl_Event_Register.BatchPrintedDateTime = DateTime.Now;
                objTbl_Event_Register.CertPrintedBy = UserName;
                objTbl_Event_Register.CertPrintedDateTime = DateTime.Now;
                objTbl_Event_Register.RegisteredBy = UserName;
                objTbl_Event_Register.RegisteredDate = DateTime.Now;
                objTbl_Event_Register.CreatedBy = 1;
                objTbl_Event_Register.CreatedDate = DateTime.Now;
                objTbl_Event_Register.BARCODE = TID.ToString() + "" + MainEventID.ToString() + "" + SubEventID.ToString() + "" + objTbl_Event_Register.ContactMyID.ToString();
                ViewState["Contact"] = objTbl_Event_Register.ContactMyID;
                ViewState["COM"] = MainEventID;
                ViewState["CON"] = SubEventID;

                DB.Tbl_Event_Register.AddObject(objTbl_Event_Register);
                DB.SaveChanges();

                ViewState["ContactMYID"] = objTbl_Event_Register.ContactMyID;
            }

        }
        protected void btnsaveContinue_Click(object sender, EventArgs e)
        {

            if (btnsaveContinue.Text == "Save & Continue")
            {
                saveandcontinue();
            }
            else if (btnsaveContinue.Text == "Update & Continue")
            {
                if (ViewState["MainEveID"] != null && ViewState["myID"] != null && ViewState["ContactMYID"] != null)
                {
                    int MEventID = Convert.ToInt32(ViewState["MainEveID"]);
                    int myID = Convert.ToInt32(ViewState["myID"]);
                    int ContactMYID = Convert.ToInt32(ViewState["ContactMYID"]);
                    Database.Tbl_Event_Register objTbl_Event_Register = DB.Tbl_Event_Register.Single(p => p.TenantID == TID && p.EventID == MEventID && p.MyID == myID && p.ContactMyID == ContactMYID);
                    objTbl_Event_Register.RegisteredAs = Convert.ToInt32(drpRegisteredAs.SelectedValue);
                    objTbl_Event_Register.RegistrationID = txtRegistrationID.Text;
                    objTbl_Event_Register.Attendee = txtContact.Text;
                    objTbl_Event_Register.CompanyName = txtSearch.Text;
                    objTbl_Event_Register.PositionName = txtPosition.Text;
                    if (FileUpload1.HasFile)
                    {
                        string path = FileUpload1.FileName;
                        FileUpload1.SaveAs(Server.MapPath("~/Master/Img/" + path));
                        objTbl_Event_Register.IMG = path;
                    }
                    objTbl_Event_Register.MobileNo = txtMobile.Text;
                    objTbl_Event_Register.Email = txtEmailId.Text;
                    objTbl_Event_Register.Busphone = txtBussinessPhone.Text;
                    objTbl_Event_Register.Address = txtAddress.Text;

                    DB.SaveChanges();
                }
            }

        }
        public void Payment(string link, string panal)
        {
            PayMethod.Style.Add("display", link);
            Paytype.Attributes["class"] = panal;
            //Payment("none", "expand");
            //Payment("block", "collapse");
        }

        protected void btnsaveprint_Click(object sender, EventArgs e)
        {
            if (btnsaveprint.Text == "Update & Print")
            {
                if (ViewState["MainEveID"] != null && ViewState["myID"] != null && ViewState["ContactMYID"] != null)
                {
                    int MEventID = Convert.ToInt32(ViewState["MainEveID"]);
                    int myID = Convert.ToInt32(ViewState["myID"]);
                    int ContactMYID = Convert.ToInt32(ViewState["ContactMYID"]);
                    Database.Tbl_Event_Register objTbl_Event_Register = DB.Tbl_Event_Register.Single(p => p.TenantID == TID && p.EventID == MEventID && p.MyID == myID && p.ContactMyID == ContactMYID);


                    objTbl_Event_Register.PaidBy = Convert.ToInt32(drpPaydby.SelectedValue);
                    objTbl_Event_Register.AmountPaid = Convert.ToDecimal(txtAmountPaid.Text);
                    objTbl_Event_Register.PaymentReference = txtPaymentReference.Text;
                    objTbl_Event_Register.Notes = txtNotes.Text;

                    int MYTRANSID = Convert.ToInt32(objTbl_Event_Register.PaymentTermsId);
                    int PaymentTermsId = Convert.ToInt32(drpPaydby.SelectedValue);
                    string CounterID = "0";
                    int LID = 1;
                    decimal Amount = Convert.ToDecimal(txtAmountPaid.Text);
                    string IDRefresh = txtPaymentReference.Text;
                    Classes.EcommAdminClass.insertICTRPayTerms_HD(TID, MYTRANSID, PaymentTermsId, CounterID, LID, 0, Amount, IDRefresh, null, null, null, 0, 0, "1", 0, false, false, false, false, null, null, null, null, null);

                    ViewState["MainEveID"] = null;
                    ViewState["myID"] = null;
                    ViewState["ContactMYID"] = null;
                    btnAdd.Text = "Add New";
                    btnAdd.ValidationGroup = "ss";
                    drpMyID.Items.Clear();
                    DB.SaveChanges();
                    //Clear();
                    //lblMsg.Text = "Data Edit Successfully";
                    //pnlSuccessMsg.Visible = true;
                    BindData();
                    Readonly();
                    BarCodee();
                    UpdatePanel7.Update();
                }
            }
            else if (btnsaveprint.Text == "Save & Print")
            {
                saveandcontinue();
                int contactmyid = Convert.ToInt32(ViewState["ContactMYID"]);

                int Maineveid = Convert.ToInt32(drpEventID.SelectedValue);
                int DetailID = Convert.ToInt32(drpMyID.SelectedValue);

                int MYTRANSID = DB.ICTR_HD.Where(p => p.TenentID == TID).Count() > 0 ? Convert.ToInt32(DB.ICTR_HD.Where(p => p.TenentID == TID).Max(p => p.MYTRANSID)) + 1 : 1;
                Database.Tbl_Event_Register objEVNPayment = DB.Tbl_Event_Register.Single(p => p.TenantID == TID && p.EventID == Maineveid && p.MyID == DetailID && p.ContactMyID == contactmyid);
                objEVNPayment.PaymentTermsId = MYTRANSID;

                decimal compid = 0;
                string SystemName = "EVN";
                string MainTranType = "";
                string TransType = "Event Payment In";
                int transid = 7101;
                int transsubid = 710101;
                if (drpCompanyName.SelectedValue == "" || drpCompanyName.SelectedValue == "0")
                    compid = 20;
                else
                    compid = Convert.ToDecimal(drpCompanyName.SelectedValue);

                int compid1 = 0;
                if (drpCompanyName.SelectedValue == "" || drpCompanyName.SelectedValue == "0")
                    compid1 = 20;
                else
                    compid1 = Convert.ToInt32(drpCompanyName.SelectedValue);

                string PERIOD_CODE = "201802";
                DateTime TRANSDATE = DateTime.Now;
                string REFERENCE = txtPaymentReference.Text;
                string NOTES = txtNotes.Text;
                string USERID = ((USER_MST)Session["USER"]).FIRST_NAME;
                bool Active = true;
                string DatainserStatest = "Add";

                Classes.EcommAdminClass.insertICTR_HD(TID, MYTRANSID, 1, 1, MainTranType, TransType, transid, transsubid, SystemName, compid, compid, "L", PERIOD_CODE, "0", 0, "0", 0, 0, 0, "0", "0", "0", 0, TRANSDATE, REFERENCE, NOTES, 0, "1", "1", "1", "1", "1", "1", USERID, Active, compid1, TRANSDATE, TRANSDATE, TRANSDATE, 0, 0, "DE", 0, DatainserStatest, "0", "0", "0", 0, "Event", "0", 0);

                //Insert ICTRPayTerms_HD
                int PaymentTermsId = Convert.ToInt32(drpPaydby.SelectedValue);
                string CounterID = "0";
                int LID = 1;
                decimal Amount = Convert.ToDecimal(txtAmountPaid.Text);
                string IDRefresh = txtPaymentReference.Text;
                Classes.EcommAdminClass.insertICTRPayTerms_HD(TID, MYTRANSID, PaymentTermsId, CounterID, LID, 0, Amount, IDRefresh, null, null, null, 0, 0, "1", 0, false, false, false, false, null, null, null, null, null);

                BarCodee();
                btnAdd.Text = "Add New";
                btnAdd.ValidationGroup = "ss";
                drpMyID.Items.Clear();
                DB.SaveChanges();
                Readonly();
                BindData();
            }
        }
        public void BarCodee()
        {
            if (ViewState["Contact"] != null && ViewState["COM"] != null && ViewState["CON"] != null)
            {
                int Contactmyod = Convert.ToInt32(ViewState["Contact"]);
                int MainEventID = Convert.ToInt32(ViewState["COM"]);
                int SubEventID = Convert.ToInt32(ViewState["CON"]);
                var List = DB.Tbl_Event_Register.Where(p => p.TenantID == TID && p.EventID == MainEventID && p.MyID == SubEventID && p.ContactMyID == Contactmyod).ToList();
                foreach (Database.Tbl_Event_Register item in List)
                {
                    string str = Server.MapPath("~/Master/Img/" + item.BARCODE.Trim() + ".png");
                    System.Drawing.Image myimg = MakeBarcodeImage(item.BARCODE.Trim(), 4, true);
                    string barCode = item.BARCODE.Trim();
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
                                    File.WriteAllBytes(Server.MapPath("~/Master/Img/" + barCode + ".png"), byteImage);
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

                ViewState["Contact"] = null;
                ViewState["COM"] = null;
                ViewState["CON"] = null;
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

        protected void LinkButton10_Click(object sender, EventArgs e)
        {
            string id = txtsearch123.Text.Trim().ToString();
            List<Database.Tbl_Event_Register> List = DB.Tbl_Event_Register.Where(p => p.TenantID == TID && (p.Attendee.ToUpper().Contains(id.ToUpper()) || p.CompanyName.ToUpper().Contains(id.ToUpper()) || p.MobileNo.ToUpper().Contains(id.ToUpper()) || p.Email.ToUpper().Contains(id.ToUpper()))).ToList();
            int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
            int Totalrec = List.Count();
            ((AcmMaster)Page.Master).Loadlist(Showdata, take, Skip, ChoiceID, lblShowinfEntry, btnPrevious1, btnNext1, Listview1, ListView2, Totalrec, List);
        }


















    }
}
