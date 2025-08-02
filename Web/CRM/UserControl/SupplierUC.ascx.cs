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
    public partial class SupplierUC : System.Web.UI.UserControl
    {
        Database.CallEntities DB = new Database.CallEntities();
        tblsetupsalesh objtblsetupsalesh = new tblsetupsalesh();
        int TID, LID, UID, EMPID, userID1, userTypeid = 0;
        int Transid = 2101;
        int Transsubid = 21011;
        string LangID, CURRENCY = "126";
        decimal CURRENTCONVRATE = Convert.ToDecimal(0);
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
            //txtserach.Visible = false;
            lblSupplierNames.Visible = false;
            lblMobileNOs.Visible = false;
            lblemails.Visible = false;
            BusPhones.Visible = false;
            //Label1.Visible = false;
            int CUNTRYID = Convert.ToInt32(CURRENCY);//int CUNTRYID = Convert.ToInt32(DB.USER_DTL.Single(p => p.USER_DETAIL_ID == userTypeid && p.TenentID == TID).COUNTRY);           


            if (txtSupplierName.Text == "" || txtMobileNO.Text == "" || txtEMAIL.Text == "" || txtBusPhone1.Text == "")
            {
                if (txtSupplierName.Text == "")
                {
                    lblSupplierNames.Visible = true;

                }
                if (txtMobileNO.Text == "")
                {
                    lblMobileNOs.Visible = true;

                }
                if (txtEMAIL.Text == "")
                {
                    lblemails.Visible = true;

                }
                if (txtBusPhone1.Text == "")
                {
                    BusPhones.Visible = true;

                }
                ModalPopupExtender1.Show();
            }
            else
            {
                if (DB.TBLCOMPANYSETUPs.Where(p => p.COMPNAME1 == txtSupplierName.Text && p.TenentID == TID).Count() < 1)
                {
                    TBLCOMPANYSETUP obj_Compay = new TBLCOMPANYSETUP();
                    obj_Compay.TenentID = TID;
                    obj_Compay.COMPID = DB.TBLCOMPANYSETUPs.Where(p => p.TenentID == TID).Count() > 0 ? Convert.ToInt32(DB.TBLCOMPANYSETUPs.Where(p => p.TenentID == TID).Max(p => p.COMPID) + 1) : 1;
                    obj_Compay.COMPNAME1 = txtSupplierName.Text;
                    obj_Compay.COMPNAME2 = Translate(txtSupplierName.Text, "ar");
                    obj_Compay.COMPNAME3 = Translate(txtSupplierName.Text, "fr");
                    obj_Compay.COUNTRYID = CUNTRYID;
                    obj_Compay.PHYSICALLOCID = "KWT";
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
                    obj_Compay.CompanyType = "82002";
                    obj_Compay.Approved = 1;
                    obj_Compay.SALER = true;
                    obj_Compay.Active = "Y";
                    DB.TBLCOMPANYSETUPs.AddObject(obj_Compay);
                    DB.SaveChanges();

                    txtSupplierName.Text = "";
                    txtMobileNO.Text = "";
                    txtEMAIL.Text = "";
                    txtBusPhone1.Text = "";
                    txtAddress1.Text = "";
                }
                else
                {
                    Label1.Visible = true;
                    ModalPopupExtender1.Show();
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
    }
}