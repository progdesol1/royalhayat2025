using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Database;
using System.Data;
using System.Net;
using System.Transactions;
using System.IO;
using System.Web.Configuration;
using System.Data.SqlClient;
using AjaxControlToolkit;
using System.Web.Services;

namespace Web.UserControl
{
    public partial class InvoiceItemList : System.Web.UI.UserControl
    {
        CallEntities DB = new CallEntities();
        List<Database.ICTRPayTerms_HD> TempICTRPayTerms_HD = new List<Database.ICTRPayTerms_HD>();
        // List<ICIT_BR_BIN> ListICIT_BR_BIN = new List<ICIT_BR_BIN>();
        // List<ICIT_BR_Perishable> ListICIT_BR_Perishable = new List<ICIT_BR_Perishable>();
        //PropertyFile objProFile = new PropertyFile();
        public static DataTable dt_PurQuat;
        List<Database.tblsetupsalesh> Listtblsetupsalesh = new List<tblsetupsalesh>();
        tblsetupsalesh objtblsetupsalesh = new tblsetupsalesh();
        MYCOMPANYSETUP objMyCompany = new MYCOMPANYSETUP();
        bool FirstFlag, ClickFlag = true;
        int TID, LID, stMYTRANSID, UID, EMPID, Transid, Transsubid, CID = 0;
        string LangID, CURRENCY, USERID, Crypath = "";
        decimal CURRENTCONVRATE = Convert.ToDecimal(0);
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionLoad();
            if (!IsPostBack)
            {
                FistTimeLoad();
            }
        }
        public void SessionLoad()
        {

            TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            LID = Convert.ToInt32(((USER_MST)Session["USER"]).LOCATION_ID);
            UID = Convert.ToInt32(((USER_MST)Session["USER"]).USER_ID);
            EMPID = Convert.ToInt32(((USER_MST)Session["USER"]).EmpID);
            LangID = Session["LANGUAGE"].ToString();


        }
        public void FistTimeLoad()
        {


            FirstFlag = false;
            // Remain ACM Work
        }
        protected void btnaddamunt_Click(object sender, EventArgs e)
        {
            ICTRPayTerms_HD objEco_ICCATEGORY = new ICTRPayTerms_HD();
            TempICTRPayTerms_HD.Add(objEco_ICCATEGORY);
            TempICTRPayTerms_HD = (List<Database.ICTRPayTerms_HD>)ViewState["TempEco_ICCATEGORY"];
            ICTRPayTerms_HD objEco_ICEXTRACOST = new ICTRPayTerms_HD();
            TempICTRPayTerms_HD.Add(objEco_ICEXTRACOST);
            ViewState["TempEco_ICCATEGORY"] = TempICTRPayTerms_HD;
            Repeater3.DataSource = TempICTRPayTerms_HD;
            Repeater3.DataBind();
        }

        protected void Repeater3_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Label lblOHType = (Label)e.Item.FindControl("lblOHType");

                DropDownList drppaymentMeted = (DropDownList)e.Item.FindControl("drppaymentMeted");
                Classes.EcommAdminClass.getdropdown(drppaymentMeted, TID, "Payment", "Method", "Inventeri", "REFTABLE");
                //Classes.EcommAdminClass.getdropdown(ddlUOM, TID, "", "", "", "REFTABLE");
                //drppaymentMeted.DataSource = DB.REFTABLE.Where(p => p.REFTYPE == "Payment" && p.REFSUBTYPE == "Method");
                //drppaymentMeted.DataTextField = "REFNAME1";
                //drppaymentMeted.DataValueField = "REFID";
                //drppaymentMeted.DataBind();
                drppaymentMeted.SelectedValue = "1250001";
                if (lblOHType.Text != "" && lblOHType.Text != "0")
                {
                    int ID = Convert.ToInt32(lblOHType.Text);
                    drppaymentMeted.SelectedValue = ID.ToString();
                }
            }
        }
    }
}