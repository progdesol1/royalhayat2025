using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Database;

namespace Web.Sales
{
    public partial class Sales_Leads : System.Web.UI.Page
    {
        CallEntities DB = new CallEntities();
        bool FirstFlag = true;
        int TID, LID, UID, EMPID, Transid, Transsubid, userID1, userTypeid, CID = 0;
        string LangID, CURRENCY, USERID, Crypath = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionLoad();
            if (!IsPostBack)
            {
                FistTimeLoad();
                BindCountryDdl();
                BindStateDdl(0);
            }
        }
        public void FistTimeLoad()
        {
            FirstFlag = false;
            // Remain ACM Work
        }

        public void SessionLoad()
        {
            string Ref = ((Sales_Master)Page.Master).SessionLoad1(TID, LID, UID, EMPID, LangID);

            string[] id = Ref.Split(',');
            TID = Convert.ToInt32(id[0]);
            LID = Convert.ToInt32(id[1]);
            UID = Convert.ToInt32(id[2]);
            EMPID = Convert.ToInt32(id[3]);
            LangID = id[4].ToString();
            userID1 = ((USER_MST)Session["USER"]).USER_ID;
            userTypeid = Convert.ToInt32(((USER_MST)Session["USER"]).USER_DETAIL_ID);

        }

        public void BindCountryDdl()
        {
            try
            {
                ddlCountry.DataSource = DB.tblCOUNTRies.Where(p => p.Active == "Y" && p.TenentID ==TID );
                ddlCountry.DataTextField = "COUNAME1";
                ddlCountry.DataValueField = "COUNTRYID";
                ddlCountry.DataBind();
                ddlCountry.Items.Insert(0, "--Country--");

                ddlPriority.DataSource = DB.REFTABLEs.Where(p => p.TenentID == TID && p.REFTYPE == "ACTVTY" && p.REFSUBTYPE == "PRIORITY");
                ddlPriority.DataTextField = "REFNAME1";
                ddlPriority.DataValueField = "REFID";
                ddlPriority.DataBind();
                ddlPriority.Items.Insert(0, "--Priority--");
            }
            catch (Exception ex)
            {
                // Web.ECOMM .WebMsgBox.Show(ex.Message);
            }

        }
        public void BindStateDdl(int CountryId)
        {
            try
            {

                ddlState.DataSource = DB.tblStates.Where(sm => sm.COUNTRYID == CountryId);
                ddlState.DataTextField = "MYNAME1";
                ddlState.DataValueField = "StateID";
                ddlState.DataBind();
                ddlState.Items.Insert(0, "--State--");

            }
            catch (Exception ex)
            {
                // ERPNew.WebMsgBox.Show(ex.Message);
            }

        }

        protected void ddlCountry_SelectedIndexChanged1(object sender, EventArgs e)
        {
            try
            {
                if (ddlCountry.SelectedItem.Text == "--Country--")
                {
                    ddlState.Items.Clear();
                }
                else
                {
                    int CountryId = Convert.ToInt32(ddlCountry.SelectedValue);
                    BindStateDdl(CountryId);
                }
                ddlState.Items.Insert(0, "--State--");

            }
            catch (Exception ex)
            {
                // ERPNew.WebMsgBox.Show(ex.Message);
            }

        }
    }
}