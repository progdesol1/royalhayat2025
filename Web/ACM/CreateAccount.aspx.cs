using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Configuration;
using System.Configuration;
using Classes;
using Database;
using System.Web;

namespace Web.ACM
{
    public partial class CreateAccount : System.Web.UI.Page
    {
        Database.CallEntities DB = new Database.CallEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FillContractorID();
            }

        }
        public void cleane()
        {
            txtcompany.Text = "";
            txtusername.Text = "";           
            txtnumuser.Text = "";
            txtEmail.Text = "";
            txtmobile.Text = "";
            drpPackage.SelectedIndex = 0;
            DrpContry.SelectedIndex = 0;
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            pnlMSG.Visible = pnlErrorMsg.Visible = false;
            if (txtnumuser.Text != null && txtEmail.Text != null)
            {
                if(DB.NewCompaniySetup_Tryel.Where(p=>p.MobileNo == txtmobile.Text).Count() < 1)
                {
                    if (DB.NewCompaniySetup_Tryel.Where(p => p.EmailID.ToUpper() == txtEmail.Text.ToUpper()).Count() < 1)
                    {
                        int Myid = 0;
                        string Copmany = txtcompany.Text;
                        string UserName = txtusername.Text;
                        int Packge = Convert.ToInt32(drpPackage.SelectedValue);
                        int NumOfUser = Convert.ToInt32(txtnumuser.Text);
                        string email = txtEmail.Text;
                        string Mobile = txtmobile.Text;
                        int CountryID = Convert.ToInt32(DrpContry.SelectedValue);
                        string DatainserStatest = "Insert";
                        Classes.EcommAdminClass.insertNewComapnySetupTry(Myid, Copmany, UserName, Packge, NumOfUser, email, Mobile, CountryID, DatainserStatest);
                        cleane();
                        pnlErrorMsg.Visible = true;
                        lblerrmsg.Text = "Your Registration Successfull...";
                    }
                    else
                    {
                        pnlMSG.Visible = true;
                        lblMSG.Text = "Kindly Not Receive Your Registration, Email_ID allReady Exist...";
                    }
                }
                else
                {
                    pnlMSG.Visible = true;
                    lblMSG.Text = "Kindly Not Receive Your Registration, Mobile allReady Exist...";
                }
            }                      
        }
        public void FillContractorID()
        {
            //drpcrupID.Items.Insert(0, new ListItem("-- Select --", "0"));drpcrupID.DataSource = DB.0;drpcrupID.DataTextField = "0";drpcrupID.DataValueField = "0";drpcrupID.DataBind();
            List<Database.tblCOUNTRY> CountryList = DB.tblCOUNTRies.Where(p => p.Active == "Y" && p.TenentID == 0).OrderBy(m => m.COUNAME1).ToList();
            DrpContry.DataSource = CountryList;//DB.tblCOUNTRies.Where(p=>p.TenentID == 0);
            DrpContry.DataTextField = "COUNAME1";
            DrpContry.DataValueField = "COUNTRYID";
            DrpContry.DataBind();
            DrpContry.Items.Insert(0, new ListItem("-- Residing Country --", "0"));
            //Classes.EcommAdminClass.getdropdown(DrpContry, 0, "", "", "", "tblCOUNTRY");

            drpPackage.DataSource = DB.REFTABLEs.Where(p => p.TenentID == 0 && p.REFTYPE == "function" && p.REFSUBTYPE == "financial");
            drpPackage.DataTextField = "REFNAME1";
            drpPackage.DataValueField = "REFID";
            drpPackage.DataBind();
            drpPackage.Items.Insert(0, new ListItem("-- Select a Package --", "0"));
            //Classes.EcommAdminClass.getdropdown(drpPackage, 0, "function", "financial", "", "REFTABLE");
        }


    }
}