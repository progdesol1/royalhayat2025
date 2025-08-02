using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Database;
namespace Web.ACM
{
    public partial class EditActivityData : System.Web.UI.Page
    {
        Database.CallEntities DB = new Database.CallEntities();
     
        protected void Page_Load(object sender, EventArgs e)
        {
            int TenenatID = 0;
            int LocationID = 0;
            int CID = 0;
            if (Request.QueryString["REFNO"] != null)
            {
                int REFID = Convert.ToInt32(Request.QueryString["REFNO"]);

                //k//Database.CRMMainActivity obj_MainActivity = DB.CRMMainActivities.SingleOrDefault(p => p.ACTIVITYCODE == REFID);
                //k//Database.CRMActivity obj_Activity = DB.CRMActivities.SingleOrDefault(p => p.TenantID == TenenatID && p.ACTIVITYCODE == REFID);
                //k//if (obj_MainActivity.COMPID != null)
                //k//{
                    //k//drpComid.SelectedValue = obj_MainActivity.COMPID.ToString();
                //k//}
                //k//if (obj_MainActivity.ACTIVITYCODE != null)
                //k//{
                    //k//drpActivitycode.SelectedValue = obj_MainActivity.ACTIVITYCODE.ToString();
                //k//}

                //txtMylineno.Text=obj_MainActivity.l
              //  txtUSERCODE.Text = obj_MainActivity.USERCODE;
                //k//txtReferenceNo.Text = obj_MainActivity.Reference;
                //k//txtActivityPerform.Text = obj_MainActivity.ACTIVITYA;
                //k//txtREMINDERNOTE.Text = obj_MainActivity.REMINDERNOTE;
                //k//txtESTCOST.Text = obj_MainActivity.ESTCOST.ToString();
              //  txtGROUPCODE.Text = obj_MainActivity.GROUPCODE;
                //k//txtUSERCODEENTERED.Text = obj_MainActivity.USERCODEENTERED;
                //k//txtUPDTTIME.Text = Convert.ToDateTime(obj_MainActivity.UPDTTIME).ToShortDateString();
               // txtUSERCODE.Text = obj_MainActivity.USERCODE;
                //if (obj_MainActivity.MYPERSONNEL == "N")
                //{
                //    ckbMypersonnel.Checked = false;
                //}
                //else
                //{
                //    ckbMypersonnel.Checked = true;
                //}
                //if (obj_MainActivity.AMIGLOBAL == "N")
                //{
                //    ckbAmiglobal.Checked = false;
                //}
                //else
                //{
                //    ckbAmiglobal.Checked = true;
                //}
                ViewState["Edit"] = REFID;
            }
        }
    }
}