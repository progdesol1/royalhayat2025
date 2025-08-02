using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Database;
using System.Data.SqlClient;
using System.Configuration;
using Classes;

namespace Web.ACM
{
    public partial class WinPOS_Registration : System.Web.UI.Page
    {
        int Tenent = 0;
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CRMNewEntitiesNew"].ConnectionString);
        SqlCommand command2 = new SqlCommand();
        CallEntities DB = new CallEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if(ViewState["Tenent"] != null)
            {
                Tenent = Convert.ToInt32(ViewState["Tenent"]);
                txttenent.Text = Tenent.ToString();
            }
            else
            {
                Tenent = DB.Win_mycompanysetup_winapp.Count() > 0 ? Convert.ToInt32(DB.Win_mycompanysetup_winapp.Max(p => p.TenentID) + 1) : 1;
                ViewState["Tenent"] = Tenent;
                txttenent.Text = Tenent.ToString();
            }
            
            if (!IsPostBack)
            {
                BindData();
                btnstore.Visible = false;
                btnTERM.Visible = false;
                btnRegFinal.Visible = false;
            }
        }

        protected void btnCRD_Click(object sender, EventArgs e)
        {            
            tab_meta.Attributes["class"] = "tab-pane active";
            tab_general.Attributes["class"] = "tab-pane";
            tab_images.Attributes["class"] = "tab-pane";
            tab_reviews.Attributes["class"] = "tab-pane";

            tabcrd.Attributes["class"] = "";
            tabstd.Attributes["class"] = "active";
            tabtrm.Attributes["class"] = "";
            tabtrge.Attributes["class"] = "";
            btnstore.Visible = true;
            Progress.Attributes["style"] = "width:25%;";

           
        }

        public void BindData()
        {
            drpcountry.DataSource = DB.tblCOUNTRies.Where(p => p.TenentID == 1 && p.Active == "Y");
            drpcountry.DataTextField = "COUNAME1";
            drpcountry.DataValueField = "COUNTRYID";
            drpcountry.DataBind();
            drpcountry.Items.Insert(0, new ListItem("-- Select --", "0"));
            drpcountry.SelectedValue = "126";
        }

        public void insertLive(int Tenent, string COMPNAME1, string COMPNAME2, string COMPNAME3, int COUNTRYID, string Mac_Addr, string DefaultLanguage)
        {
            string Shopid = txtshopid.Text.ToString();
            string UploadDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");

            DateTime InstallDate = DateTime.Now;
            DateTime Exp_Date = InstallDate.AddDays(30);

            string sqlinsert = " insert into Win_mycompanysetup_winapp " + "(TenentID, Shopid, TenentGroupID ,COMPNAME1 , COMPNAME2 ,  COMPNAME3, COUNTRYID , Mac_Addr,DefaultLanguage,AllowUser,Uploadby ,UploadDate ,SynID,installDate,ExpireDate) " + " values (" + Tenent + ",'" + Shopid + "'," + 1 + ",'" + COMPNAME1 + "', N'" + COMPNAME2 + "','" + COMPNAME3 + "'," + COUNTRYID + ",'" + "" + "','" + DefaultLanguage + "', '1' ,'" + "" + "' , '" + UploadDate + "'  , 1,'" + InstallDate + "','" + Exp_Date + "')";
            command2 = new SqlCommand(sqlinsert, con);
            con.Open();
            command2.ExecuteReader();
            con.Close();
        }

        protected void btnstore_Click(object sender, EventArgs e)
        {
            tab_general.Attributes["class"] = "tab-pane";
            tab_meta.Attributes["class"] = "tab-pane";
            tab_images.Attributes["class"] = "tab-pane active";
            tab_reviews.Attributes["class"] = "tab-pane";

            tabcrd.Attributes["class"] = "";
            tabstd.Attributes["class"] = "";
            tabtrm.Attributes["class"] = "active";
            tabtrge.Attributes["class"] = "";
            btnTERM.Visible = true;
            Progress.Attributes["style"] = "width:50%;";
        }

        protected void btnTERM_Click(object sender, EventArgs e)
        {
            tab_general.Attributes["class"] = "tab-pane";
            tab_meta.Attributes["class"] = "tab-pane";
            tab_images.Attributes["class"] = "tab-pane";
            tab_reviews.Attributes["class"] = "tab-pane active";

            tabcrd.Attributes["class"] = "";
            tabstd.Attributes["class"] = "";
            tabtrm.Attributes["class"] = "";
            tabtrge.Attributes["class"] = "active";
            btnRegFinal.Visible = true;
            Progress.Attributes["style"] = "width:75%;";
        }

        protected void btnRegFinal_Click(object sender, EventArgs e)
        {
            try
            {
                int Tenentt = Tenent;
                string COMPNAME1 = txtCNameENG.Text;
                string COMPNAME2 = txtCNameAra.Text;
                string COMPNAME3 = txtCNameFra.Text;
                int COUNTRYID = Convert.ToInt32(drpcountry.SelectedValue);

                string DefaultLanguage = DrpDefaultLang.SelectedValue;
                string UploadDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                string UserName = txtUSERREGuername.Text.Trim().ToString();
                string Mac_Addr = UserName;
                insertLive(Tenentt, COMPNAME1, COMPNAME2, COMPNAME3, COUNTRYID, Mac_Addr, DefaultLanguage);

                string Sqlinsert = "insert into Win_storeconfig (TenentID, companyname, companyaddress,companyphone,vatno,web,vatrate,disrate,footermsg,FaceBook,Twitter,Insta,DbPath,ImgPath,InvAddtionalLine,Logo,Uploadby ,UploadDate ,SynID) " +
                                       "  values( " + Tenentt + ",'" + txtstoreCMDName.Text + "', '" + txtStoreADD.Text + "', '" + txtStorePhone.Text + "', '" + txtstoreTAXREG.Text + "','" + txtstoreWEBsite.Text + "', " +
                                       " '" + txtTAXPER.Text + "', '" + txtstoreDISPER.Text + "' , '" + txtstoreFooter.Text + "',   " +
                                       " '" + txtstorefacebook.Text + "', '" + txtstorefacebook.Text + "' , '" + txtstoreinsta.Text + "',   " +
                                       " '" + "" + "', '" + "" + "','" + txtstoreAddLine.Text + "','" + "POS53.png" + "','" + UserName + "' , '" + UploadDate + "'  , 1);";
                command2 = new SqlCommand(Sqlinsert, con);
                con.Open();
                command2.ExecuteReader();
                con.Close();

                int TerminalID = DB.Win_tbl_terminalLocation.Where(p => p.TenentID == Tenentt).Count() > 0 ? Convert.ToInt32(DB.Win_tbl_terminalLocation.Where(p => p.TenentID == Tenentt).Max(p => p.ID) + 1) : 1;
                string sqterminal = " insert into Win_tbl_terminalLocation " +
                                               "(TenentID, ID, Shopid,Terminal_Type, CompanyName, Branchname , Location ,Phone , Email ,  Web, VAT , Dis , VATRegiNo , Footermsg,FaceBook,Twitter,Insta,InvAddtionalLine,DbPath,ImgPath,syncAfter,Uploadby ,UploadDate ,SynID) " +
                                               " values (" + Tenentt + ", " + TerminalID + ",'" + txtshopid.Text + "' , '" + drpTERMtype.SelectedValue + "' , N'" + txtstoreCMDName.Text + "' , N'" + txtTERMName.Text + "' , " +
                                                " N'" + txtTERMadd.Text + "' , '" + txtTERMPhone.Text + "' , '" + txtTERMEmail.Text + "' ," +
                                                " '" + txtTERMWebsite.Text + "',  '" + txtTERMTaxPer.Text + "', " +
                                                " '" + txtTEMPDisPer.Text + "' , '" + txtTERMTaxReg.Text + "',  N'" + txtTERMFooter.Text + "' ," +
                                                " '" + txtTermFacebook.Text + "','" + txtTERMtwitter.Text + "','" + txtTERMinsta.Text + "', N'" + txtTERMinvline.Text + "'," +
                                                " '" + "" + "',  '" + "" + "'," + txtTERMsyncafter.Text + ",'" + UserName + "' , '" + UploadDate + "'  , 1)";

                command2 = new SqlCommand(sqterminal, con);
                con.Open();
                command2.ExecuteReader();
                con.Close();

                string check = CheckBox1.Checked == true ? "1" : "0";
                string sqlLogI = " insert into Win_tblsetupsalesh (TenentID, locationID,AllowMinusQty,Uploadby ,UploadDate ,SynID) values ('" + Tenentt + "' ,1," + check + ",'" + UserName + "' , '" + UploadDate + "'  , 1 )";
                command2 = new SqlCommand(sqlLogI, con);
                con.Open();
                command2.ExecuteReader();
                con.Close();

                int userid = DB.Win_usermgt.Where(p => p.TenentID == Tenentt).Count() > 0 ? Convert.ToInt32(DB.Win_usermgt.Where(p => p.TenentID == Tenentt).Max(p => p.id) + 1) : 1;
                string dob = Convert.ToDateTime(txtUSERREGdob.Text).ToString("MM/dd/yyyy");
                string sql1 = "insert into Win_usermgt (TenentID,id, Name, Father_name, Address, Email , Contact, DOB , Username , password , usertype , position , imagename, Shopid,Uploadby ,UploadDate ,SynID) " +
                                 "  values(" + Tenentt + "," + userid + ",'" + txtUSERREGname.Text + "', '" + txtUSERREGLname.Text + "', '" + txtaddress.Text + "', '" + txtUSERREGEmail.Text + "', " +
                                 " '" + txtUSERREGcontact.Text + "',  '" + dob + "', '" + UserName + "', '" + txtUSERREGpassword.Text + "', " +
                                 " '" + drpregusertype.SelectedValue + "', '" + drpregusertype.SelectedItem.ToString() + "' , '" + "user.png" + "' , '" + txtshopid.Text + "','" + UserName + "' , '" + UploadDate + "'  , 1)";

                command2 = new SqlCommand(sql1, con);
                con.Open();
                command2.ExecuteReader();
                con.Close();



                string UName = UserName;
                string UPass = txtUSERREGpassword.Text;
                string Compname = txtCNameENG.Text;
                string compname2 = txtCNameAra.Text;
                string compname3 = txtCNameFra.Text;
                string CommanDefauLANG = DrpDefaultLang.SelectedValue;
                string CompAdd = txtStoreADD.Text;
                string CompPhone = txtStorePhone.Text;
                string CompWebside = txtstoreWEBsite.Text;
                string userLastName = txtUSERREGLname.Text;
                string useradd = txtaddress.Text;
                string usermobile = txtUSERREGcontact.Text;
                string userbirthdate = dob;
                string useremail = txtUSERREGEmail.Text;
                string userusertype = drpregusertype.SelectedItem.ToString();
                Classes.WinPOS.WinPOSUSER(Tenentt, UName, UPass, Compname, compname2, compname3, CommanDefauLANG, CompAdd, CompPhone, CompWebside, userLastName, useradd, usermobile, userbirthdate, useremail, userusertype);
                Classes.WinPOS.Win_registation(Tenentt, UName, UPass, Compname, compname2, compname3, CommanDefauLANG, CompAdd, CompPhone, CompWebside, userLastName, useradd, usermobile, userbirthdate, useremail, userusertype, true);


                pnlErrorMsg.Visible = true;
                lblerrmsg.Text = "Registration Successfully Done, you Have a 30 days Trial Version. Go Login page and login";
            }
            catch
            {
                string UName = txtUSERREGuername.Text.Trim().ToString();
                string UPass = txtUSERREGpassword.Text;
                string Compname = txtCNameENG.Text;
                string compname2 = txtCNameAra.Text;
                string compname3 = txtCNameFra.Text;
                string CommanDefauLANG = DrpDefaultLang.SelectedValue;
                string CompAdd = txtStoreADD.Text;
                string CompPhone = txtStorePhone.Text;
                string CompWebside = txtstoreWEBsite.Text;
                string userLastName = txtUSERREGLname.Text;
                string useradd = txtaddress.Text;
                string usermobile = txtUSERREGcontact.Text;
                string userbirthdate = Convert.ToDateTime(txtUSERREGdob.Text).ToString("MM/dd/yyyy");
                string useremail = txtUSERREGEmail.Text;
                string userusertype = drpregusertype.SelectedItem.ToString();
                Classes.WinPOS.Win_registation(Tenent, UName, UPass, Compname, compname2, compname3, CommanDefauLANG, CompAdd, CompPhone, CompWebside, userLastName, useradd, usermobile, userbirthdate, useremail, userusertype, false);

                pnlErrorMsg.Visible = true;
                lblerrmsg.Text = "opsss Something Wrong, Your Registration Is Fail";
            }
        }

        protected void txtCNameENG_TextChanged(object sender, EventArgs e)
        {
            txtstoreCMDName.Text = txtCNameENG.Text;
        }

    }
}