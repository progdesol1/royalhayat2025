using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Database;
using System.Net;
using System.IO;

using System.Threading;
using System.Drawing;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Transactions;

using GenCode128;
using System.Data.Objects;
using QRCoder;

namespace Web.CRM.UserControl
{
    public partial class EventUC : System.Web.UI.UserControl
    {
        //#endregion
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
        int TID, LID, UID, EMPID, userID1, userTypeid = 0;
        string LangID = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionLoad();
            if (!IsPostBack)
            {
                BindComp();
            }
        }
        public void SessionLoad()
        {
            if (Session["USER"] != null)
            {
                TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
                LID = Convert.ToInt32(((USER_MST)Session["USER"]).LOCATION_ID);
                UID = Convert.ToInt32(((USER_MST)Session["USER"]).USER_ID);
                EMPID = Convert.ToInt32(((USER_MST)Session["USER"]).EmpID);
                LangID = Session["LANGUAGE"].ToString();
                userID1 = ((USER_MST)Session["USER"]).USER_ID;
                userTypeid = Convert.ToInt32(((USER_MST)Session["USER"]).USER_DETAIL_ID);
            }
            else
            {
                TID = Convert.ToInt32(2);
                LID = Convert.ToInt32(1);
                UID = Convert.ToInt32(11405);
                EMPID = Convert.ToInt32(0);
                Session["LANGUAGE"] = "en-US";
                LangID = Session["LANGUAGE"].ToString();
                userID1 = 0;
                userTypeid = 0;
            }
        }
        public void CleanData()
        {
            txtcompneySerch.Text = txtSupplierName1.Text = txtMobileNO.Text = txtAddress1.Text = txtEMAIL.Text = txtBusPhone1.Text = "";
            imgtxtBusPhone1.Visible = imgtxtBusPhone1no.Visible = imgtxtEMAIL.Visible = imgtxtEMAILno.Visible = imgtxtMobile.Visible = imgtxtMobileNO.Visible = imgtxtSupplierName.Visible = imgimgtxtSupplierNameNo.Visible = false;
            txtserach.Visible = false;
            lbButton1.Visible = true;
            PanelCustomer.Visible = false;
        }
        protected void lbButton1_Click(object sender, EventArgs e)
        {
            using (TransactionScope scope = new TransactionScope())
            {

                string UserName = "Event";
                if (Session["USER"] != null)
                {
                    UserName = ((USER_MST)Session["USER"]).FIRST_NAME.ToString();
                }
                txtserach.Visible = false;
                int CUNTRYID = 126;
                int CompanyID = 20;
                int MainEventID = 0;
                int SubEventID = 0;
                int Posision = 99999;
                string POName = "Not Found";
                //if (DB.USER_DTL.Where(p => p.USER_DETAIL_ID == userTypeid && p.TenentID == TID).Count() > 0)
                //{
                //    CUNTRYID = Convert.ToInt32(DB.USER_DTL.Single(p => p.USER_DETAIL_ID == userTypeid && p.TenentID == TID).COUNTRY);
                //}
                var EDate = DateTime.Now.Date;
                if (DB.View_EventMainDetail.Where(p => p.TenantID == TID && p.ToDate >= EDate).Count() > 0)
                {
                    if (Request.QueryString["event"] != null)
                    {
                        int EID = Convert.ToInt32(Request.QueryString["event"]);
                        if (DB.View_EventMainDetail.Where(p => p.TenantID == TID && p.ToDate >= EDate && p.EventID == EID).Count() > 0)
                        {
                            Database.View_EventMainDetail obj = DB.View_EventMainDetail.Where(p => p.TenantID == TID && p.ToDate >= EDate && p.EventID == EID).OrderBy(p => p.ToDate).First();
                            MainEventID = Convert.ToInt32(obj.EventID);
                            SubEventID = Convert.ToInt32(obj.MyID);
                        }
                    }
                }
                if (ViewState["Modify"] != null)
                {
                    if (lbButton1.Text == "Edit")
                    {
                        int AttID = Convert.ToInt32(ViewState["Modify"]);

                        Database.TBLCONTACT obj_Compay = DB.TBLCONTACTs.Single(p => p.TenentID == TID && p.ContactMyID == AttID);
                        //CompanyID = Convert.ToInt32(drpCompnay.SelectedValue);
                        //obj_Compay.TenentID = TID;
                        //int Conract = DB.TBLCONTACTs.Where(p => p.TenentID == TID).Count() > 0 ? Convert.ToInt32(DB.TBLCONTACTs.Where(p => p.TenentID == TID).Max(p => p.ContactMyID) + 1) : 1;
                        //obj_Compay.ContactMyID = Conract;
                        //obj_Compay.CONTACTID = Conract;
                        obj_Compay.PersName1 = txtSupplierName1.Text;
                        obj_Compay.PersName2 = Translate(txtSupplierName1.Text, "ar");
                        obj_Compay.PersName3 = Translate(txtSupplierName1.Text, "fr");
                        obj_Compay.PHYSICALLOCID = "WKT";
                        obj_Compay.COUNTRYID = CUNTRYID;
                        obj_Compay.EMAIL1 = txtEMAIL.Text;
                        obj_Compay.ADDR1 = txtAddress1.Text;
                        obj_Compay.BUSPHONE1 = txtBusPhone1.Text;
                        obj_Compay.MOBPHONE = txtMobileNO.Text;
                        obj_Compay.Active = "Y";
                        obj_Compay.BUYER = true;
                        obj_Compay.BARCODE = TID.ToString() + AttID.ToString();
                        obj_Compay.COMPANYID = Convert.ToInt32(drpCompnay.SelectedValue);
                        obj_Compay.Instuctor_Username = drpCompnay.SelectedItem.ToString().Replace("-", "").TrimEnd().TrimStart();
                        if (avatarUploadd.HasFile)  //FileUpload code
                        {
                            string path = avatarUploadd.FileName;
                            avatarUploadd.SaveAs(Server.MapPath("images/" + path));
                            obj_Compay.IMG = path;
                        }
                        DB.TBLCONTACTs.AddObject(obj_Compay);

                        Database.Tbl_Event_Register objTbl_Event_Register = DB.Tbl_Event_Register.Single(p => p.TenantID == TID && p.ContactMyID == AttID && p.EventID == MainEventID && p.MyID == SubEventID);
                        //objTbl_Event_Register.TenantID = TID;
                        //objTbl_Event_Register.EventID = MainEventID;
                        //objTbl_Event_Register.MyID = SubEventID;
                        if (objTbl_Event_Register.RegisteredAs == 0 || objTbl_Event_Register.RegisteredAs == null)
                            objTbl_Event_Register.RegisteredAs = Convert.ToInt32(33311);
                        if (objTbl_Event_Register.RegistrationID == "" || objTbl_Event_Register.RegistrationID == null)
                            objTbl_Event_Register.RegistrationID = AttID.ToString();
                        objTbl_Event_Register.CompanyId = Convert.ToInt32(drpCompnay.SelectedValue);
                        objTbl_Event_Register.CompanyName = drpCompnay.SelectedItem.ToString();
                        //objTbl_Event_Register.ContactMyID = Conract;
                        objTbl_Event_Register.Attendee = txtSupplierName1.Text;
                        objTbl_Event_Register.PositionName = POName;
                        objTbl_Event_Register.positionId = Posision;
                        objTbl_Event_Register.MobileNo = txtMobileNO.Text;
                        objTbl_Event_Register.Busphone = txtBusPhone1.Text;
                        objTbl_Event_Register.Email = txtEMAIL.Text;
                        objTbl_Event_Register.Address = txtAddress1.Text;
                        if (objTbl_Event_Register.PaidBy == 0 || objTbl_Event_Register.PaidBy == null)
                            objTbl_Event_Register.PaidBy = Convert.ToInt32(2151);
                        if (objTbl_Event_Register.AmountPaid == 0 || objTbl_Event_Register.AmountPaid == null)
                            objTbl_Event_Register.AmountPaid = Convert.ToDecimal(0);
                        if (objTbl_Event_Register.PaymentReference == "" || objTbl_Event_Register.PaymentReference == null)
                            objTbl_Event_Register.PaymentReference = "Notfound";
                        if (objTbl_Event_Register.Notes == "" || objTbl_Event_Register.Notes == null)
                            objTbl_Event_Register.Notes = "Notfound";
                        //objTbl_Event_Register.PaymentTermsId = Convert.ToInt32(drpPaymentTermsId.SelectedValue);                                
                        //objTbl_Event_Register.AttendedTime = Convert.ToDateTime(txtAttendedTime.Text);
                        objTbl_Event_Register.BatchPrintedBy = "By Attendee";
                        objTbl_Event_Register.BatchPrintedDateTime = DateTime.Now;
                        objTbl_Event_Register.CertPrintedBy = UserName;
                        objTbl_Event_Register.CertPrintedDateTime = DateTime.Now;
                        objTbl_Event_Register.RegisteredBy = UserName;
                        objTbl_Event_Register.RegisteredDate = DateTime.Now;
                        objTbl_Event_Register.CreatedBy = 1;
                        objTbl_Event_Register.CreatedDate = DateTime.Now;
                        //objTbl_Event_Register.BARCODE = TID.ToString() + "" + MainEventID.ToString() + "" + SubEventID.ToString() + "" + Conract.ToString();
                        if (avatarUploadd.HasFile)
                        {
                            string path = avatarUploadd.FileName;
                            avatarUploadd.SaveAs(Server.MapPath("~/Master/Img/" + path));
                            objTbl_Event_Register.IMG = path;
                        }
                        DB.Tbl_Event_Register.AddObject(objTbl_Event_Register);
                        lbButton1.Text = "Add New";
                        ViewState["Modify"] = null;
                        CleanData();
                        BindComp();
                        return;
                    }
                }

                if (DB.TBLCONTACTs.Where(p => p.PersName1 == txtSupplierName1.Text && p.TenentID == TID).Count() < 1)
                {
                    if (drpCompnay.SelectedValue == "0")
                    {
                        //txtserach.Visible = true;
                        //txtserach.Text = "please Select Company...";
                        CompanyID = 20;
                    }
                    else
                    {
                        CompanyID = Convert.ToInt32(drpCompnay.SelectedValue);
                    }
                    if (drpItManager.SelectedValue == "0")
                    {
                        //txtserach.Visible = true;
                        //txtserach.Text = "please Select Position...";
                        Posision = 99999;
                        POName = "Not Found";
                    }
                    else
                    {
                        Posision = Convert.ToInt32(drpItManager.SelectedValue);
                        POName = drpCompnay.SelectedItem.ToString();
                    }
                    if (txtSupplierName1.Text == "")
                    {
                        txtserach.Visible = true;
                        txtserach.Text = "please Fill Name of Attendees...";
                        return;
                    }
                    if (txtMobileNO.Text == "")
                    {
                        txtserach.Visible = true;
                        txtserach.Text = "please Fill Mobile NO...";
                        return;
                    }
                    if (txtEMAIL.Text == "")
                    {
                        txtserach.Visible = true;
                        txtserach.Text = "please Fill Email...";
                        return;
                    }
                    //if (txtBusPhone1.Text == "")
                    //{
                    //    txtserach.Visible = true;
                    //    txtserach.Text = "please Fill Bus Phone...";
                    //    return;
                    //}
                    if (txtAddress1.Text == "")
                    {
                        txtserach.Visible = true;
                        txtserach.Text = "please Fill Address...";
                        return;
                    }

                    Database.TBLCONTACT obj_Compay = new Database.TBLCONTACT();
                    //CompanyID = Convert.ToInt32(drpCompnay.SelectedValue);
                    obj_Compay.TenentID = TID;
                    int Conract = DB.TBLCONTACTs.Where(p => p.TenentID == TID).Count() > 0 ? Convert.ToInt32(DB.TBLCONTACTs.Where(p => p.TenentID == TID).Max(p => p.ContactMyID) + 1) : 1;
                    obj_Compay.ContactMyID = Conract;
                    obj_Compay.CONTACTID = Conract;
                    obj_Compay.PersName1 = txtSupplierName1.Text;
                    obj_Compay.PersName2 = Translate(txtSupplierName1.Text, "ar");
                    obj_Compay.PersName3 = Translate(txtSupplierName1.Text, "fr");
                    obj_Compay.PHYSICALLOCID = "WKT";
                    obj_Compay.COUNTRYID = CUNTRYID;
                    obj_Compay.EMAIL1 = txtEMAIL.Text;
                    obj_Compay.ADDR1 = txtAddress1.Text;
                    obj_Compay.BUSPHONE1 = txtBusPhone1.Text;
                    obj_Compay.MOBPHONE = txtMobileNO.Text;
                    obj_Compay.Active = "Y";
                    obj_Compay.BUYER = true;
                    obj_Compay.BARCODE = TID.ToString() + Conract.ToString();
                    obj_Compay.COMPANYID = CompanyID;
                    obj_Compay.Instuctor_Username = drpCompnay.SelectedItem.ToString().Replace("-", "").TrimEnd().TrimStart();
                    if (avatarUploadd.HasFile)  //FileUpload code
                    {
                        string path = avatarUploadd.FileName;
                        avatarUploadd.SaveAs(Server.MapPath("images/" + path));
                        obj_Compay.IMG = path;
                    }
                    DB.TBLCONTACTs.AddObject(obj_Compay);

                    Database.Tbl_Event_Register objTbl_Event_Register = new Database.Tbl_Event_Register();
                    objTbl_Event_Register.TenantID = TID;
                    objTbl_Event_Register.EventID = MainEventID;
                    objTbl_Event_Register.MyID = SubEventID;
                    objTbl_Event_Register.RegisteredAs = Convert.ToInt32(33311);
                    objTbl_Event_Register.RegistrationID = Conract.ToString();
                    objTbl_Event_Register.CompanyId = CompanyID;
                    objTbl_Event_Register.CompanyName = drpCompnay.SelectedItem.ToString();
                    objTbl_Event_Register.ContactMyID = Conract;
                    objTbl_Event_Register.Attendee = txtSupplierName1.Text;
                    objTbl_Event_Register.PositionName = POName;
                    objTbl_Event_Register.positionId = Posision;
                    objTbl_Event_Register.MobileNo = txtMobileNO.Text;
                    objTbl_Event_Register.Busphone = txtBusPhone1.Text;
                    objTbl_Event_Register.Email = txtEMAIL.Text;
                    objTbl_Event_Register.Address = txtAddress1.Text;
                    objTbl_Event_Register.PaidBy = Convert.ToInt32(2151);
                    objTbl_Event_Register.AmountPaid = Convert.ToDecimal(0);
                    objTbl_Event_Register.PaymentReference = "Notfound";
                    objTbl_Event_Register.Notes = "Notfound";
                    //objTbl_Event_Register.PaymentTermsId = Convert.ToInt32(drpPaymentTermsId.SelectedValue);                                
                    //objTbl_Event_Register.AttendedTime = Convert.ToDateTime(txtAttendedTime.Text);
                    objTbl_Event_Register.BatchPrintedBy = "By Attendee";
                    objTbl_Event_Register.BatchPrintedDateTime = DateTime.Now;
                    objTbl_Event_Register.CertPrintedBy = UserName;
                    objTbl_Event_Register.CertPrintedDateTime = DateTime.Now;
                    objTbl_Event_Register.RegisteredBy = UserName;
                    objTbl_Event_Register.RegisteredDate = DateTime.Now;
                    objTbl_Event_Register.CreatedBy = 1;
                    objTbl_Event_Register.CreatedDate = DateTime.Now;
                    objTbl_Event_Register.BARCODE = TID.ToString() + "" + MainEventID.ToString() + "" + SubEventID.ToString() + "" + Conract.ToString();
                    if (avatarUploadd.HasFile)
                    {
                        string path = avatarUploadd.FileName;
                        avatarUploadd.SaveAs(Server.MapPath("~/Master/Img/" + path));
                        objTbl_Event_Register.IMG = path;
                    }
                    DB.Tbl_Event_Register.AddObject(objTbl_Event_Register);

                    string JOB = drpItManager.SelectedItem.ToString();
                    try
                    {
                        tblCONTACTBu obj_ContactBUS = new tblCONTACTBu();
                        obj_ContactBUS.TenentID = TID;
                        obj_ContactBUS.ContactMyID = DB.tblCONTACTBus.Where(p => p.TenentID == TID).Count() > 0 ? Convert.ToInt32(DB.tblCONTACTBus.Where(p => p.TenentID == TID).Max(p => p.ContactMyID) + 1) : 1;
                        obj_ContactBUS.CompID = CompanyID;
                        obj_ContactBUS.PhysicalLocID = "KWT";
                        obj_ContactBUS.Country = CUNTRYID.ToString();
                        obj_ContactBUS.JobTitle = JOB.Replace("-", "").TrimStart().TrimEnd();
                        obj_ContactBUS.BusPhone1 = txtBusPhone1.Text;
                        obj_ContactBUS.email2 = txtEMAIL.Text;
                        obj_ContactBUS.PrimLanguge = "English";
                        obj_ContactBUS.webpage = "http://www.google.com";
                        DB.tblCONTACTBus.AddObject(obj_ContactBUS);
                        //DB.SaveChanges();
                    }
                    catch
                    {

                    }
                    DB.SaveChanges();
                    CleanData();
                    BindComp();

                    //bracode generater 1                   
                    //barcode generater 2
                    try
                    {
                        //var List = DB.TBLCONTACTs.Where(p => p.ContactMyID == Conract && p.BUYER == true && p.Active == "Y").ToList();
                        //foreach (Database.TBLCONTACT item in List)
                        //var List = DB.Tbl_Event_Register.Where(p => p.TenantID == TID && p.EventID == MainEventID && p.MyID == SubEventID && p.ContactMyID == Conract).ToList();
                        //foreach (Database.Tbl_Event_Register item in List)
                        //{
                        //    string str = Server.MapPath("~/CRM/images/" + item.BARCODE.Trim() + ".png");
                        //    if (!File.Exists(str))
                        //    {
                        //        string barCode = item.BARCODE.Trim();
                        //        System.Web.UI.WebControls.Image imgBarCode = new System.Web.UI.WebControls.Image();
                        //        using (Bitmap bitMap = new Bitmap(barCode.Length * 40, 80))
                        //        {
                        //            using (Graphics graphics = Graphics.FromImage(bitMap))
                        //            {
                        //                Font oFont = new Font("IDAutomationHC39M", 16);
                        //                PointF point = new PointF(2f, 2f);
                        //                SolidBrush blackBrush = new SolidBrush(Color.Black);
                        //                SolidBrush whiteBrush = new SolidBrush(Color.White);
                        //                graphics.FillRectangle(whiteBrush, 0, 0, bitMap.Width, bitMap.Height);
                        //                graphics.DrawString("*" + barCode + "*", oFont, blackBrush, point);
                        //            }
                        //            using (MemoryStream ms = new MemoryStream())
                        //            {
                        //                bitMap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                        //                byte[] byteImage = ms.ToArray();

                        //                Convert.ToBase64String(byteImage);
                        //                imgBarCode.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(byteImage);
                        //                try
                        //                {
                        //                    File.WriteAllBytes(Server.MapPath("images/" + barCode + ".png"), byteImage);
                        //                }
                        //                catch (System.Exception ex)
                        //                {
                        //                    Response.Write(ex.InnerException);
                        //                }
                        //            }
                        //        }
                        //    }
                        //    Classes.Toastr.ShowToast(Page, Classes.Toastr.ToastType.Success, "Barcode genarate Successfully", "Barcode genarate 1", Classes.Toastr.ToastPosition.BottomLeft);
                        //}

                        var List = DB.Tbl_Event_Register.Where(p => p.TenantID == TID && p.EventID == MainEventID && p.MyID == SubEventID && p.ContactMyID == Conract).ToList();
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
                    }
                    catch (Exception ex)
                    {
                        Classes.Toastr.ShowToast(Page, Classes.Toastr.ToastType.Error, "Error Occured During Barcode genarate 1 process !<br>" + ex.ToString(), "Barcode genarate 2", Classes.Toastr.ToastPosition.TopCenter);
                    }

                    //TBLCOMPANYSETUP obj_Compay = new TBLCOMPANYSETUP();
                    //obj_Compay.TenentID = TID;
                    //obj_Compay.COMPID = DB.TBLCOMPANYSETUPs.Where(p => p.TenentID == TID).Count() > 0 ? Convert.ToInt32(DB.TBLCOMPANYSETUPs.Where(p => p.TenentID == TID).Max(p => p.COMPID) + 1) : 1;
                    //obj_Compay.COMPNAME1 = txtSupplierName1.Text;
                    //obj_Compay.COMPNAME2 = Translate(txtSupplierName1.Text, "ar");
                    //obj_Compay.COMPNAME3 = Translate(txtSupplierName1.Text, "fr");
                    //obj_Compay.PHYSICALLOCID = "WKT";
                    //obj_Compay.COUNTRYID = CUNTRYID;
                    //obj_Compay.EMAIL = txtEMAIL.Text;
                    //obj_Compay.ADDR1 = txtAddress1.Text;
                    //obj_Compay.BUSPHONE1 = txtBusPhone1.Text;
                    //obj_Compay.MOBPHONE = txtMobileNO.Text;
                    //obj_Compay.Approved = 1;
                    //obj_Compay.Active = "Y";
                    //obj_Compay.BUYER = true;
                    //obj_Compay.BARCODE = TID.ToString() + obj_Compay.COMPID.ToString();
                    //if (avatarUploadd.HasFile)  //FileUpload code
                    //{
                    //    string path = avatarUploadd.FileName;
                    //    avatarUploadd.SaveAs(Server.MapPath("images/" + path));
                    //    obj_Compay.Avtar = path;
                    //}
                    //DB.TBLCOMPANYSETUPs.AddObject(obj_Compay);
                    //DB.SaveChanges();
                }
                else
                {
                    Classes.Toastr.ShowToast(Page, Classes.Toastr.ToastType.Error, "'Attendees Is Already Exist...'", "Error!", Classes.Toastr.ToastPosition.TopCenter);
                    //ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "myscript", "alert('Attendees Already Exist.');", true);
                }
                scope.Complete();//To Commit
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
                //plBarCode.Controls.Add(imgBarCode1);
            }
        }
        public void BindComp()
        {
            //List<TBLCOMPANYSETUP> listComp = DB.TBLCOMPANYSETUPs.Where(p => p.TenentID == TID && p.BUYER == true && p.Active == "Y").ToList();
            //drpCompnay.DataSource = listComp;
            //drpCompnay.DataTextField = "COMPNAME1";
            //drpCompnay.DataValueField = "COMPID";
            //drpCompnay.DataBind();
            drpCompnay.Items.Clear();
            drpCompnay.Items.Insert(0, new ListItem("-- Not Found --", "20"));

            List<Tbl_Position_Mst> List = DB.Tbl_Position_Mst.Where(p => p.Active == true && p.TenentID == TID).OrderBy(m => m.PositionName).ToList();
            drpItManager.DataSource = List;
            drpItManager.DataTextField = "PositionName";
            drpItManager.DataValueField = "PositionID";
            drpItManager.DataBind();
            drpItManager.Items.Insert(0, new ListItem("-- Not Found --", "99999"));
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

        protected void txtSupplierName_TextChanged(object sender, EventArgs e)
        {

            int CUNTRYID = 126;
            //if (TID != 7)
            //    CUNTRYID = Convert.ToInt32(DB.USER_DTL.Single(p => p.USER_DETAIL_ID == userTypeid && p.TenentID == TID).COUNTRY);

            var List = DB.TBLCONTACTs.Where(p => p.PersName1 == txtSupplierName1.Text && p.TenentID == TID);
            if (List.Count() < 1)
            {
                imgtxtSupplierName.Visible = true;
                imgimgtxtSupplierNameNo.Visible = false;
                lbButton1.Visible = true;
                PanelCustomer.Visible = false;
            }
            else
            {
                imgimgtxtSupplierNameNo.Visible = true;
                lbButton1.Visible = false;
                imgtxtSupplierName.Visible = false;
                PanelCustomer.Visible = true;
                txtserach.Visible = true;
                txtserach.Text = "Attendees Is Allready Exist...";
                CustomerList.DataSource = List;
                CustomerList.DataBind();
            }
            txtMobileNO.Focus();
        }

        protected void txtMobileNO_TextChanged(object sender, EventArgs e)
        {
            int CUNTRYID = 126;
            //if (TID != 7)
            //    CUNTRYID = Convert.ToInt32(DB.USER_DTL.Single(p => p.USER_DETAIL_ID == userTypeid && p.TenentID == TID).COUNTRY);
            var List = DB.TBLCONTACTs.Where(p => p.PersName1 == txtSupplierName1.Text && p.TenentID == TID);
            if (List.Count() < 1)
            {
                imgtxtMobile.Visible = true;
                imgtxtMobileNO.Visible = false;
                lbButton1.Visible = true;
                PanelCustomer.Visible = false;
            }
            else
            {
                imgtxtMobileNO.Visible = true;
                lbButton1.Visible = false;
                imgtxtMobile.Visible = false;
                PanelCustomer.Visible = true;
                CustomerList.DataSource = List;
                CustomerList.DataBind();
            }
            txtEMAIL.Focus();
        }

        protected void txtEMAIL_TextChanged(object sender, EventArgs e)
        {
            int CUNTRYID = 126;
            //if (TID != 7)
            //    CUNTRYID = Convert.ToInt32(DB.USER_DTL.Single(p => p.USER_DETAIL_ID == userTypeid && p.TenentID == TID).COUNTRY);
            var List = DB.TBLCONTACTs.Where(p => p.PersName1 == txtSupplierName1.Text && p.TenentID == TID);
            if (List.Count() < 1)
            {
                imgtxtEMAIL.Visible = true;
                imgtxtEMAILno.Visible = false;
                lbButton1.Visible = true;
                PanelCustomer.Visible = false;
            }
            else
            {
                imgtxtEMAILno.Visible = true;
                lbButton1.Visible = false;
                imgtxtEMAIL.Visible = false;
                PanelCustomer.Visible = true;
                CustomerList.DataSource = List;
                CustomerList.DataBind();
            }
            txtBusPhone1.Focus();
        }

        protected void txtBusPhone1_TextChanged(object sender, EventArgs e)
        {
            int CUNTRYID = 126;
            //if (TID != 7)
            //    CUNTRYID = Convert.ToInt32(DB.USER_DTL.Single(p => p.USER_DETAIL_ID == userTypeid && p.TenentID == TID).COUNTRY);
            var List = DB.TBLCONTACTs.Where(p => p.PersName1 == txtSupplierName1.Text && p.TenentID == TID);
            if (List.Count() < 1)
            {
                imgtxtBusPhone1.Visible = true;
                imgtxtBusPhone1no.Visible = false;
                lbButton1.Visible = true;
                PanelCustomer.Visible = false;
            }
            else
            {
                imgtxtBusPhone1no.Visible = true;
                lbButton1.Visible = false;
                imgtxtBusPhone1.Visible = false;
                PanelCustomer.Visible = true;
                CustomerList.DataSource = List;
                CustomerList.DataBind();
            }
            txtAddress1.Focus();
        }

        protected void CustomerList_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (e.CommandName == "Modify")
            {
                string COMPID = e.CommandArgument.ToString();
                int AttendeeID = Convert.ToInt32(COMPID);
                Database.TBLCONTACT objattendee = DB.TBLCONTACTs.Single(p => p.TenentID == TID && p.ContactMyID == AttendeeID);
                txtSupplierName1.Text = objattendee.PersName1;
                txtMobileNO.Text = objattendee.MOBPHONE;
                txtEMAIL.Text = objattendee.EMAIL1;
                txtBusPhone1.Text = objattendee.BUSPHONE1;
                txtAddress1.Text = objattendee.ADDR1;
                ViewState["Modify"] = AttendeeID;
                lbButton1.Visible = true;
                lbButton1.Text = "Edit";
                //Response.Redirect("/CRM/CompanyMaster.aspx?COMPID=" + COMPID + "&Mode=Write");
                //string url = "/CRM/ContactMaster.aspx?COMPID=" + COMPID + "&Mode=Write";
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "OpenWindow", "window.open('" + url + "','_newtab');", true);
            }
        }

        //protected void lblBarCode_Click(object sender, EventArgs e)
        //{
        //    //Response.Redirect("/CRM/Barcodetest.aspx");
        //}

        protected void lkbCustomerN1_Click(object sender, EventArgs e)
        {
            string id1 = txtcompneySerch.Text.ToString();
            //List<TBLCOMPANYSETUP> list1 = DB.TBLCOMPANYSETUPs.Where(p => p.TenentID == TID && (p.COMPNAME1.ToUpper().Contains(id1.ToUpper()) || p.COMPNAME2.ToUpper().Contains(id1.ToUpper()) || p.EMAIL1.ToUpper().Contains(id1.ToUpper())) && p.Active == "Y").OrderBy(p => p.COMPID).ToList();
            if (id1 == "")
            {
                txtserach.Visible = true;
                txtserach.Text = " No Records Founds";
            }
            else
            {
                List<TBLCOMPANYSETUP> list1 = DB.TBLCOMPANYSETUPs.Where(p => (p.COMPNAME1.ToUpper().Contains(id1.ToUpper()) || p.MOBPHONE.Contains(id1) || p.BUSPHONE1.Contains(id1) || p.ADDR1.ToUpper().Contains(id1.ToUpper()) || p.EMAIL.ToUpper().Contains(id1.ToUpper()) || p.BARCODE.Contains(id1)) && p.TenentID == TID && p.CompanyType == "82005").ToList();

                drpCompnay.DataSource = list1.OrderBy(p => p.COMPNAME1);
                drpCompnay.DataTextField = "COMPNAME1";
                drpCompnay.DataValueField = "COMPID";
                drpCompnay.DataBind();
                drpCompnay.Items.Insert(0, new ListItem("-- Not Found --", "20"));
                ViewState["List"] = 123;

                int Count = list1.Count();
                if (Count > 0)
                {
                    txtserach.Visible = true;
                    txtserach.Text = Count + " Companies found use the Drop Down to select one from";
                }
                else
                {
                    txtserach.Visible = true;
                    txtserach.Text = " No Records Founds";
                    BindComp();
                }
            }
        }

        protected void drpCompnay_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (ViewState["List"] != null)
            //{
            //    int CID = Convert.ToInt32(drpCompnay.SelectedValue);
            //    List<Database.tblCONTACTBu> ComId = DB.tblCONTACTBus.Where(p => p.TenentID == TID && p.CompID == CID).ToList();

            //    drpItManager.DataSource = ComId;
            //    drpItManager.DataTextField = "JobTitle";
            //    drpItManager.DataValueField = "ContactMyID";
            //    drpItManager.DataBind();
            //    //drpItManager.Items.Insert(0, new ListItem("-- Select --", "0"));
            //    TBLCONTACT OBJCOM = DB.TBLCONTACTs.Single(p => p.TenentID == TID && p.COMPANYID == CID && p.Active == "Y");

            //    txtSupplierName1.Text = OBJCOM.PersName1.ToString();
            //    txtMobileNO.Text = OBJCOM.MOBPHONE.ToString();
            //    txtEMAIL.Text = OBJCOM.EMAIL1.ToString();
            //    txtBusPhone1.Text = OBJCOM.BUSPHONE1.ToString();
            //    txtAddress1.Text = OBJCOM.ADDR1.ToString();
            //    ViewState["List"] = null;
            //}
        }

        protected void imgRefresh_Click(object sender, ImageClickEventArgs e)
        {
            CleanData();
            BindComp();
        }



    }
}