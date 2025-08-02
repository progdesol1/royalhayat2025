using System;
//using SaasDAL;
using System.Linq;
//using SaasBAL;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Transactions;
using System.Globalization;
using System.Resources;
using System.Threading;
using System.Linq.Expressions;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Database;

namespace Web.CRM
{
    public partial class tbltranstype : System.Web.UI.Page
    {
        // DALtbltranstype objDALValue = new DALtbltranstype();
        //CommonSelectDelete objCommonSelDel = new CommonSelectDelete();
        //ERPDataLinqToSqlDataContext dbContext = new ERPDataLinqToSqlDataContext();
        Database.CallEntities DB = new Database.CallEntities();
        string[] lfillform;
        string popupScript = "";
        bool FirstFlag, ClickFlag = true;
        int TID, LID, stMYTRANSID, UID, MID, EMPID, Transid, Transsubid, CID = 0;
        string LangID, CURRENCY, USERID, Crypath = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionLoad();
            if (!IsPostBack)
            {
                FistTimeLoad();
                
                fillGrid();
                bindSystem();
            }
            Session["Pagename"] = "AdminForm";
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

        protected void lnkbtndelete_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton ln = (LinkButton)sender;
                lfillform = ln.CommandArgument.Split(',');
                int OID = Convert.ToInt32(lfillform[1]);
                Database.tbltranstype obj_tbltranstype = DB.tbltranstypes.SingleOrDefault(p => p.transid == OID && p.TenentID == TID);
                obj_tbltranstype.Active = "D";
                DB.SaveChanges();
                //objCommonSelDel.CommonDeleteWithTenant("prc_acm_common_update", lfillform[0].ToString(), "tbltranstype", "ACTIVE", "D", "transid", Convert.ToInt32(lfillform[1].ToString()), "Active");
                //   WebMsgBox.Show("Record deleted Successfully !!");
                fillGrid();
                clear();
                btnSubmit.Text = "Submit";
            }
            catch (Exception ex)
            {
                //   WebMsgBox.Show(ex.Message);
            }
        }

        protected void lnkbtn_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton ln = (LinkButton)sender;
                lfillform = ln.CommandArgument.Split(',');

                int OID = Convert.ToInt32(lfillform[1]);
                Database.tbltranstype obj_tbltranstype = DB.tbltranstypes.SingleOrDefault(p => p.transid == OID && p.TenentID == TID);
                //  System.Data.DataTable dt2 = objCommonSelDel.CommonFillConditionGridPQ("[dbo].[prc_acm_common_sel_con]", "c", "TenentID,transid,MYSYSNAME,inoutSwitch,transtype1,transtype2,transtype3,serialno,years,Active,CRUP_ID", "tbltranstype", "transid", lfillform[1].ToString(), "");
                drpMysysname.SelectedValue = obj_tbltranstype.MYSYSNAME.ToString();//dt2.Rows[0]["MYSYSNAME"].ToString();
                txttranstype1.Text = obj_tbltranstype.transtype1.ToString();
                txttranstype2.Text = obj_tbltranstype.transtype2.ToString(); //dt2.Rows[0]["transtype2"].ToString();
                txttranstype3.Text = obj_tbltranstype.transtype3.ToString(); //dt2.Rows[0]["transtype3"].ToString();
                txtyears.Text = obj_tbltranstype.years.ToString();//dt2.Rows[0]["years"].ToString();
                txtserialno.Text = obj_tbltranstype.serialno.ToString();//dt2.Rows[0]["serialno"].ToString();
                txtinoutSwitch.Text = obj_tbltranstype.inoutSwitch.ToString(); //dt2.Rows[0]["inoutSwitch"].ToString();
                hidId.Value = lfillform[1].ToString();
                btnSubmit.Text = "Update";
            }
            catch (Exception ex)
            {
                //   WebMsgBox.Show(ex.Message);
            }

        }
        protected void lnkAction_Click(object sender, EventArgs e)
        {
            try
            {
                string[] lfillform;
                LinkButton ln = (LinkButton)sender;
                lfillform = ln.CommandArgument.Split(',');
                int ID = Convert.ToInt32(lfillform[0]);
                string flag = lfillform[1];
                if (flag == "Y")
                {
                    flag = "N";
                }
                else
                {
                    flag = "Y";
                }
                //objCommonSelDel.CommonDeleteWithTenant("prc_acm_common_update", lfillform[0].ToString(), "tbltranstype", "ACTIVE", flag, "transid", Convert.ToInt32(lfillform[2].ToString()), "Active");
                //  WebMsgBox.Show("Status changed Successfully !!");
                int OID = Convert.ToInt32(lfillform[2]);
                Database.tbltranstype obj_tbltranstype = DB.tbltranstypes.SingleOrDefault(p => p.transid == OID && p.TenentID == TID);
                obj_tbltranstype.Active = "D";
                DB.SaveChanges();
                fillGrid();
                clear();
                btnSubmit.Text = "Submit";
            }
            catch (Exception ex)
            {
                //  WebMsgBox.Show(ex.Message);
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                int i = (string.IsNullOrEmpty(hidId.Value) ? -1 : Convert.ToInt32(hidId.Value));
                Database.tbltranstype objDALValue = new Database.tbltranstype();

                if (i == -1)
                {
                    int intIdt = DB.tbltranstypes.Max(u => (int)u.transid) == null ? 1 : DB.tbltranstypes.Max(u => (int)u.transid) + 1;
                    objDALValue.transid = intIdt;
                    objDALValue.Active = "Y";
                }
                else
                {
                    objDALValue.transid = i;
                }
                objDALValue.MYSYSNAME = drpMysysname.SelectedItem.Text;
                objDALValue.inoutSwitch = txtinoutSwitch.Text;
                objDALValue.transtype1 = txttranstype1.Text;
                objDALValue.transtype2 = txttranstype2.Text;
                objDALValue.transtype3 = txttranstype3.Text;
                objDALValue.serialno = txtserialno.Text;
                objDALValue.years = txtyears.Text;
                objDALValue.TenentID = TID;
                // objDALValue.CreatedBy = UID;
                // objDALValue.UpdatedBy = UID;
                int j = 1; //objDALValue.insertTable();
                if (j == 1)
                {
                    popupScript = "<script language='javascript'>" + "showSuccessToast();" + "</script>";
                    clear();
                    hidId.Value = "";
                    fillGrid();
                    btnSubmit.Text = "Submit";
                }
                else if (j == 2)
                {
                    popupScript = "<script language='javascript'>" + "showSuccessToast1();" + "</script>";
                    //WebMsgBox.Show("Role updated successfully.");
                    clear();
                    hidId.Value = "";
                    fillGrid();
                    btnSubmit.Text = "Submit";
                }
                Page.ClientScript.RegisterStartupScript(typeof(Page), "popupScript", popupScript);
            }
            catch (Exception ex)
            {
                //  WebMsgBox.Show(ex.Message);
            }

        }

        public void clear()
        {
            try
            {
                drpMysysname.SelectedIndex = 0;
                txtinoutSwitch.Text = txttranstype1.Text = txttranstype2.Text = txttranstype3.Text = txtserialno.Text = txtyears.Text = "";
            }
            catch (Exception ex)
            {
                //   WebMsgBox.Show(ex.Message);
            }
        }

        public void bindSystem()
        {
            try
            {
                // System.Data.DataTable dt = objCommonSelDel.CommonFillCondition("[dbo].[prc_acm_common_sel_con]", "DISTINCT", "MYSYSNAME", "dbo.TBLSYSTEMS", "", "");
                //if (dt.Rows.Count > 0)
                //{

                Classes.EcommAdminClass.getdropdown(drpMysysname, TID, "", "", "", "TBLSYSTEMS");
                //select * from TBLSYSTEMS

                //drpMysysname.DataSource = DB.TBLSYSTEMS.Distinct();
                //drpMysysname.DataTextField = "MYSYSNAME";
                //drpMysysname.DataValueField = "MYSYSNAME";
                //drpMysysname.DataBind();
                //drpMysysname.Items.Insert(0, "-- Select --");
                drpMysysname.SelectedIndex = -1;
                //}
                //else
                //{
                //    drpMysysname.Items.Clear();
                //    drpMysysname.Items.Insert(0, "-- Select --");
                //}
            }
            catch (Exception ex)
            {
                //  ERPNew.WebMsgBox.Show(ex.Message);
            }
        }
        public void fillGrid()
        {
            try
            {
                List<Database.tbltranstype> List_Transtype = DB.tbltranstypes.Where(p => p.TenentID == TID && p.Active == "1").ToList();
                // System.Data.DataTable dt = objCommonSelDel.CommonMethodOderFnmTnmObTenant("[dbo].[prc_getAllTable_Act_OdrWise]", "TenentID,transid,MYSYSNAME,inoutSwitch,transtype1,transtype2,transtype3,serialno,years,Active,CRUP_ID", "tbltranstype", "transid", TID);
                if (List_Transtype.Count() > 0)
                {
                    grdmstr.DataSource = List_Transtype;
                    grdmstr.DataBind();
                    for (int i = 0; i < grdmstr.Items.Count; i++)
                    {
                        LinkButton ln = (LinkButton)grdmstr.Items[i].FindControl("lnkAction");
                        Label lbl = (Label)grdmstr.Items[i].FindControl("lblGRCActive");
                        if (lbl.Text.Trim().Equals("Y") || lbl.Text.Trim().Equals("1"))
                        {
                            ln.Text = "Inactive";
                        }
                        else
                        {
                            ln.Text = "Active";
                        }
                    }
                }
                else
                {
                    grdmstr.DataBind();
                }
            }
            catch (Exception ex)
            {
                //  WebMsgBox.Show(ex.Message);
            }

        }

    }
}