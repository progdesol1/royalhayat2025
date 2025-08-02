using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Database;
using System.Net;
using System.IO;

namespace Web.CRM.UserControl
{
    public partial class Custmer : System.Web.UI.UserControl
    {
        Database.CallEntities DB = new Database.CallEntities();
        int TID, LID, UID, EMPID, userID1, userTypeid, Transid, Transsubid, CUNTRYID = 0;
        string LangID = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionLoad();
            if (!IsPostBack)
            {
                //DrpCoutry.DataSource = DB.tblCOUNTRies.Where(p => p.TenentID == TID && p.Active == "Y");
                //DrpCoutry.DataTextField = "COUNAME1";
                //DrpCoutry.DataValueField = "ISO3166_1_2LetterCode";
                //DrpCoutry.DataBind();
                //DrpState.Items.Insert(0, new ListItem("-- Select --", "0"));
                // bindState();
            }
            Database.tblsetupsalesh objtblsetupsalesh = Classes.Transaction.DEfaultSalesSetup(TID, LID, Transid, Transsubid, 10);
            CUNTRYID = objtblsetupsalesh.COUNTRYID!=null && objtblsetupsalesh.COUNTRYID!=0? Convert.ToInt32(objtblsetupsalesh.COUNTRYID):126;
        }
        public void SessionLoad()
        {
            TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            LID = Convert.ToInt32(((USER_MST)Session["USER"]).LOCATION_ID);
            UID = Convert.ToInt32(((USER_MST)Session["USER"]).USER_ID);
            EMPID = Convert.ToInt32(((USER_MST)Session["USER"]).EmpID);
            LangID = Session["LANGUAGE"].ToString();
            userID1 = ((USER_MST)Session["USER"]).USER_ID;
            userTypeid = Convert.ToInt32(((USER_MST)Session["USER"]).USER_DETAIL_ID);
            Transid = Convert.ToInt32(Request.QueryString["transid"]);
            Transsubid = Convert.ToInt32(Request.QueryString["transsubid"]);

        }
        public void bindState()
        {
            //string CNAME = DrpCoutry.SelectedItem.ToString();
            //int CID = DB.tblCOUNTRies.Single(p => p.ISO3166_1_2LetterCode == DrpCoutry.SelectedValue && p.COUNAME1 == CNAME).COUNTRYID;

            //DrpState.DataSource = DB.tblStates.Where(p => p.COUNTRYID == CID && p.ACTIVE1 == "Y");
            //DrpState.DataTextField = "MYNAME1";
            //DrpState.DataValueField = "StateID";
            //DrpState.DataBind();
            //DrpState.Items.Insert(0, new ListItem("-- Select --", "0"));
        }
        protected void lbButton1_Click(object sender, EventArgs e)
        {
            txtserach.Visible = false;
            


            //if (TID != 7)
            //    CUNTRYID = Convert.ToInt32(DB.USER_DTL.Single(p => p.USER_DETAIL_ID == userTypeid && p.TenentID == TID).COUNTRY);

            if (DB.TBLCOMPANYSETUPs.Where(p => p.COMPNAME1 == txtSupplierName.Text && p.TenentID == TID).Count() < 1)
            {
                if (txtSupplierName.Text == "")
                {
                    txtserach.Visible = true;
                    txtserach.Text = "please Select Customer Name...";
                    return;
                }
                if (txtMobileNO.Text == "")
                {
                    txtserach.Visible = true;
                    txtserach.Text = "please Select Mobile No...";
                    return;
                }
                if (txtEMAIL.Text == "")
                {
                    txtserach.Visible = true;
                    txtserach.Text = "please Select EMAIL_ID...";
                    return;
                }
                if (txtBusPhone1.Text == "")
                {
                    txtserach.Visible = true;
                    txtserach.Text = "please Select Bus Phone...";
                    return;
                }
                if (txtAddress1.Text == "")
                {
                    txtserach.Visible = true;
                    txtserach.Text = "please Select Bus Phone...";
                    return;
                }
                TBLCOMPANYSETUP obj_Compay = new TBLCOMPANYSETUP();
                obj_Compay.TenentID = TID;
                obj_Compay.COMPID = DB.TBLCOMPANYSETUPs.Where(p => p.TenentID == TID).Count() > 0 ? Convert.ToInt32(DB.TBLCOMPANYSETUPs.Where(p => p.TenentID == TID).Max(p => p.COMPID) + 1) : 1;
                obj_Compay.COMPNAME1 = txtSupplierName.Text;
                obj_Compay.COMPNAME2 = Translate(txtSupplierName.Text, "ar");
                obj_Compay.COMPNAME3 = Translate(txtSupplierName.Text, "fr");
                obj_Compay.PHYSICALLOCID = "KWT";
                obj_Compay.COUNTRYID = 126;
                obj_Compay.EMAIL = txtEMAIL.Text;
                obj_Compay.EMAIL1 = txtEMAIL.Text;
                obj_Compay.EMAIL2 = txtEMAIL.Text;
                obj_Compay.ADDR1 = txtAddress1.Text;
                obj_Compay.ADDR2 = txtAddress1.Text;
                obj_Compay.STATE = "1906";
                obj_Compay.FAX = "00000";
                obj_Compay.FAX1 = "00000";
                obj_Compay.FAX2 = "00000";
                obj_Compay.BUSPHONE1 = txtBusPhone1.Text;
                obj_Compay.BUSPHONE2 = txtBusPhone1.Text;
                obj_Compay.BUSPHONE3 = txtBusPhone1.Text;
                obj_Compay.BUSPHONE4 = txtBusPhone1.Text;
                obj_Compay.MOBPHONE = txtMobileNO.Text;
                obj_Compay.ZIPCODE = "00000";
                obj_Compay.POSTALCODE = "00000";
                obj_Compay.CITY = "79";
                obj_Compay.Approved = 1;
                obj_Compay.Active = "Y";
                obj_Compay.CompanyType = "82003";
                obj_Compay.BUYER = true;
                obj_Compay.MYPRODID = 0;
                obj_Compay.MYCONLOCID = 0;
                DB.TBLCOMPANYSETUPs.AddObject(obj_Compay);
                DB.SaveChanges();

            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "myscript", "alert('Customer Name Already Taken.');", true);
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

        protected void txtSupplierName_TextChanged(object sender, EventArgs e)
        {

            //int CUNTRYID = 126;

            //if (TID != 7)
            //    CUNTRYID = Convert.ToInt32(DB.USER_DTL.Single(p => p.USER_DETAIL_ID == userTypeid && p.TenentID == TID).COUNTRY);

            var List = DB.TBLCOMPANYSETUPs.Where(p => p.COMPNAME1 == txtSupplierName.Text && p.TenentID == TID);
            if (List.Count() < 1)
            {
                imgtxtSupplierName.Visible = true;
                imgimgtxtSupplierNameNo.Visible = false;
                lbButton1.Visible = true;

            }
            else
            {
                imgimgtxtSupplierNameNo.Visible = true;
                lbButton1.Visible = false;
                imgtxtSupplierName.Visible = false;
                PanelCustomer.Visible = true;
                CustomerList.DataSource = List;
                CustomerList.DataBind();
            }
            txtMobileNO.Focus();
        }

        protected void txtMobileNO_TextChanged(object sender, EventArgs e)
        {
            //int CUNTRYID = 126;
            //if (TID != 7)
            //    CUNTRYID = Convert.ToInt32(DB.USER_DTL.Single(p => p.USER_DETAIL_ID == userTypeid && p.TenentID == TID).COUNTRY);
            var List = DB.TBLCOMPANYSETUPs.Where(p => p.MOBPHONE == txtMobileNO.Text && p.TenentID == TID);
            if (List.Count() < 1)
            {
                imgtxtMobile.Visible = true;
                imgtxtMobileNO.Visible = false;
                lbButton1.Visible = true;
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
            //int CUNTRYID = 126;
            //if (TID != 7)
            //    CUNTRYID = Convert.ToInt32(DB.USER_DTL.Single(p => p.USER_DETAIL_ID == userTypeid && p.TenentID == TID).COUNTRY);
            var List = DB.TBLCOMPANYSETUPs.Where(p => p.EMAIL == txtEMAIL.Text && p.TenentID == TID);
            if (List.Count() < 1)
            {
                imgtxtEMAIL.Visible = true;
                imgtxtEMAILno.Visible = false;
                lbButton1.Visible = true;
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
            //int CUNTRYID = 126;
            //if (TID != 7)
            //    CUNTRYID = Convert.ToInt32(DB.USER_DTL.Single(p => p.USER_DETAIL_ID == userTypeid && p.TenentID == TID).COUNTRY);
            var List = DB.TBLCOMPANYSETUPs.Where(p => p.BUSPHONE1 == txtBusPhone1.Text && p.TenentID == TID);
            if (List.Count() < 1)
            {
                imgtxtBusPhone1.Visible = true;
                imgtxtBusPhone1no.Visible = false;
                lbButton1.Visible = true;
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
                int ComID = Convert.ToInt32(COMPID);
                //Database.TBLCOMPANYSETUP objCom = DB.TBLCOMPANYSETUPs.Single(p => p.COMPID == ComID && p.TenentID == TID);
                //txtSupplierName.Text = objCom.COMPNAME1;
                //txtMobileNO.Text = objCom.MOBPHONE;
                //txtEMAIL.Text = objCom.EMAIL;
                //txtBusPhone1.Text = objCom.BUSPHONE1;
                //txtAddress1.Text = objCom.ADDR1;
                //Response.Redirect("/CRM/CompanyMaster.aspx?COMPID=" + COMPID + "&Mode=Write");
                string url = "/CRM/CompanyMaster.aspx?COMPID=" + COMPID + "&Mode=Write";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "OpenWindow", "window.open('" + url + "','_newtab');", true);
            }
        }
    }
}